using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbappemma.Migrations
{
    public partial class hjgvgfyhj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblInscripcion");

            migrationBuilder.DropTable(
                name: "TblAlumno");

            migrationBuilder.DropTable(
                name: "TblCurso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TblUsuario",
                table: "TblUsuario");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "TblUsuario");

            migrationBuilder.DropColumn(
                name: "Rol",
                table: "TblUsuario");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "TblUsuario",
                newName: "IntentosFallidos");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "TblUsuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "IntentosFallidos",
                table: "TblUsuario",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TblUsuario",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "BloqueadoHasta",
                table: "TblUsuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TblUsuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EstaActivo",
                table: "TblUsuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "TblUsuario",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "TblUsuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "TblUsuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimoLogin",
                table: "TblUsuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblUsuario",
                table: "TblUsuario",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TblUsuario",
                table: "TblUsuario");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TblUsuario");

            migrationBuilder.DropColumn(
                name: "BloqueadoHasta",
                table: "TblUsuario");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TblUsuario");

            migrationBuilder.DropColumn(
                name: "EstaActivo",
                table: "TblUsuario");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "TblUsuario");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "TblUsuario");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "TblUsuario");

            migrationBuilder.DropColumn(
                name: "UltimoLogin",
                table: "TblUsuario");

            migrationBuilder.RenameColumn(
                name: "IntentosFallidos",
                table: "TblUsuario",
                newName: "UsuarioID");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "TblUsuario",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioID",
                table: "TblUsuario",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "TblUsuario",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rol",
                table: "TblUsuario",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TblUsuario",
                table: "TblUsuario",
                column: "UsuarioID");

            migrationBuilder.CreateTable(
                name: "TblAlumno",
                columns: table => new
                {
                    AlumnoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rut = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblAlumno", x => x.AlumnoID);
                });

            migrationBuilder.CreateTable(
                name: "TblCurso",
                columns: table => new
                {
                    CursoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Horario = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombreCurso = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCurso", x => x.CursoID);
                });

            migrationBuilder.CreateTable(
                name: "TblInscripcion",
                columns: table => new
                {
                    InscripcionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlumnoID = table.Column<int>(type: "int", nullable: false),
                    CursoID = table.Column<int>(type: "int", nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblInscripcion", x => x.InscripcionID);
                    table.ForeignKey(
                        name: "FK_TblInscripcion_TblAlumno_AlumnoID",
                        column: x => x.AlumnoID,
                        principalTable: "TblAlumno",
                        principalColumn: "AlumnoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblInscripcion_TblCurso_CursoID",
                        column: x => x.CursoID,
                        principalTable: "TblCurso",
                        principalColumn: "CursoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblInscripcion_AlumnoID",
                table: "TblInscripcion",
                column: "AlumnoID");

            migrationBuilder.CreateIndex(
                name: "IX_TblInscripcion_CursoID",
                table: "TblInscripcion",
                column: "CursoID");
        }
    }
}
