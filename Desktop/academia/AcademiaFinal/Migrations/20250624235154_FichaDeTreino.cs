using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexusGym.Migrations
{
    /// <inheritdoc />
    public partial class FichaDeTreino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemFichaDeTreinoId",
                table: "Equipamentos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FichasDeTreino",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FichasDeTreino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FichasDeTreino_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FichasDeTreino_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItensFichaDeTreino",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FichaDeTreinoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensFichaDeTreino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensFichaDeTreino_FichasDeTreino_FichaDeTreinoId",
                        column: x => x.FichaDeTreinoId,
                        principalTable: "FichasDeTreino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_ItemFichaDeTreinoId",
                table: "Equipamentos",
                column: "ItemFichaDeTreinoId");

            migrationBuilder.CreateIndex(
                name: "IX_FichasDeTreino_ClienteId",
                table: "FichasDeTreino",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_FichasDeTreino_UsuarioId",
                table: "FichasDeTreino",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensFichaDeTreino_FichaDeTreinoId",
                table: "ItensFichaDeTreino",
                column: "FichaDeTreinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamentos_ItensFichaDeTreino_ItemFichaDeTreinoId",
                table: "Equipamentos",
                column: "ItemFichaDeTreinoId",
                principalTable: "ItensFichaDeTreino",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_ItensFichaDeTreino_ItemFichaDeTreinoId",
                table: "Equipamentos");

            migrationBuilder.DropTable(
                name: "ItensFichaDeTreino");

            migrationBuilder.DropTable(
                name: "FichasDeTreino");

            migrationBuilder.DropIndex(
                name: "IX_Equipamentos_ItemFichaDeTreinoId",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "ItemFichaDeTreinoId",
                table: "Equipamentos");
        }
    }
}
