using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GerenciadorCinema.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NumeroSala = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Filmes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Diretor = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Duracao = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    SalaId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filmes_Salas_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Salas",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Filmes",
                columns: new[] { "Id", "Diretor", "Duracao", "Nome", "SalaId" },
                values: new object[] { new Guid("d6e38674-8057-48a2-9e73-224e7a0d4d16"), "Robert Zemeckis", new TimeSpan(0, 2, 22, 0, 0), "Forrest Gump", null });

            migrationBuilder.InsertData(
                table: "Salas",
                columns: new[] { "Id", "Descricao", "NumeroSala" },
                values: new object[,]
                {
                    { new Guid("48d76a83-1453-4fef-ba32-a56110e12b7e"), "Sala com projeção 3D", "Sala 3" },
                    { new Guid("9401bbb8-9499-4a9e-9475-2e61f16cb336"), "Sala VIP com assentos reclináveis e serviço de bar", "Sala 2" },
                    { new Guid("99ef33b3-ac0f-4e96-8b61-a1faae89971b"), "Sala principal com capacidade para 150 pessoas", "Sala 1" }
                });

            migrationBuilder.InsertData(
                table: "Filmes",
                columns: new[] { "Id", "Diretor", "Duracao", "Nome", "SalaId" },
                values: new object[,]
                {
                    { new Guid("23844d9c-0d76-477d-8fe6-dc10781ce0c7"), "Francis Ford Coppola", new TimeSpan(0, 2, 55, 0, 0), "O Poderoso Chefão", new Guid("99ef33b3-ac0f-4e96-8b61-a1faae89971b") },
                    { new Guid("67f4cc13-8e14-4c13-8f58-404178b62fb1"), "Chris Columbus", new TimeSpan(0, 2, 32, 0, 0), "Harry Potter e a Pedra Filosofal", new Guid("48d76a83-1453-4fef-ba32-a56110e12b7e") },
                    { new Guid("d9c2a815-3550-4726-bd8e-e2c5d77d8cf9"), "Lana e Lilly Wachowski", new TimeSpan(0, 2, 16, 0, 0), "Matrix", new Guid("9401bbb8-9499-4a9e-9475-2e61f16cb336") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filmes_SalaId",
                table: "Filmes",
                column: "SalaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filmes");

            migrationBuilder.DropTable(
                name: "Salas");
        }
    }
}
