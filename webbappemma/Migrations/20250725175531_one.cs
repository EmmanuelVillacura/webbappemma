using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webbappemma.Migrations
{
    public partial class one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblInscripcion");

            migrationBuilder.DropTable(
                name: "TblAlumno");

            migrationBuilder.DropTable(
                name: "TblCurso");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
