using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using System.Security.Cryptography;
using System.Text;

namespace webbappemma.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _connectionString;

        public AccountController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("conexion");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Home/Index1.cshtml", model);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var usuario = await connection.QueryFirstOrDefaultAsync<Usuario>(
                    "sp_AutenticarUsuario",
                    new { username = model.Username, password = model.Password },
                    commandType: CommandType.StoredProcedure);

                if (usuario == null)
                {
                    ModelState.AddModelError("", "Credenciales inválidas");
                    return View(model);
                }

                if (usuario.BloqueadoHasta.HasValue && usuario.BloqueadoHasta > DateTime.Now)
                {
                    ModelState.AddModelError("", $"Cuenta bloqueada hasta {usuario.BloqueadoHasta}");
                    return View(model);
                }

                if (!VerificarPassword(model.Password, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    var bloquear = usuario.IntentosFallidos + 1 >= 5;
                    await connection.ExecuteAsync(
                        "sp_RegistrarIntentoFallido",
                        new { id = usuario.Id, bloquear },
                        commandType: CommandType.StoredProcedure);

                    ModelState.AddModelError("", "Credenciales inválidas");
                    return View(model);
                }

                await connection.ExecuteAsync(
                    "sp_ActualizarUltimoLogin",
                    new { id = usuario.Id },
                    commandType: CommandType.StoredProcedure);

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Generar salt y hash
            var salt = GenerarSalt();
            var hash = GenerarHash(model.Password, salt);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var id = await connection.ExecuteScalarAsync<int>(
                    "sp_RegistrarUsuario",
                    new
                    {
                        username = model.Username,
                        email = model.Email,
                        password_hash = hash,
                        password_salt = salt
                    },
                    commandType: CommandType.StoredProcedure);

                // Puedes redirigir a login o mostrar mensaje de éxito
                return RedirectToAction("Login");
            }
        }

        private bool VerificarPassword(string password, string hashAlmacenado, string saltAlmacenado)
        {
            using var sha = SHA256.Create();
            var saltBytes = Convert.FromBase64String(saltAlmacenado);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordConSalt = new byte[saltBytes.Length + passwordBytes.Length];
            Buffer.BlockCopy(saltBytes, 0, passwordConSalt, 0, saltBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, passwordConSalt, saltBytes.Length, passwordBytes.Length);
            var hash = sha.ComputeHash(passwordConSalt);
            var hashBase64 = Convert.ToBase64String(hash);
            return hashBase64 == hashAlmacenado;
        }

        private string GenerarSalt()
        {
            var saltBytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private string GenerarHash(string password, string saltBase64)
        {
            using var sha = SHA256.Create();
            var saltBytes = Convert.FromBase64String(saltBase64);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var passwordConSalt = new byte[saltBytes.Length + passwordBytes.Length];
            Buffer.BlockCopy(saltBytes, 0, passwordConSalt, 0, saltBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, passwordConSalt, saltBytes.Length, passwordBytes.Length);
            var hash = sha.ComputeHash(passwordConSalt);
            return Convert.ToBase64String(hash);
        }
    }

    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool EstaActivo { get; set; }
        public int IntentosFallidos { get; set; }
        public DateTime? BloqueadoHasta { get; set; }
    }
}