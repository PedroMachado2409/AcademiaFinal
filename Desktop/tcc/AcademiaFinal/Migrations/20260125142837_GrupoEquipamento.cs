using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexusGym.Migrations
{
    /// <inheritdoc />
    public partial class GrupoEquipamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GrupoEquipamento",
                table: "Equipamentos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Mensagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdConversa = table.Column<int>(type: "integer", nullable: false),
                    ConversaId = table.Column<int>(type: "integer", nullable: true),
                    IdRemetente = table.Column<int>(type: "integer", nullable: false),
                    RemetenteId = table.Column<int>(type: "integer", nullable: true),
                    Conteudo = table.Column<string>(type: "text", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensagem_Conversas_ConversaId",
                        column: x => x.ConversaId,
                        principalTable: "Conversas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mensagem_Usuarios_RemetenteId",
                        column: x => x.RemetenteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_ConversaId",
                table: "Mensagem",
                column: "ConversaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_RemetenteId",
                table: "Mensagem",
                column: "RemetenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mensagem");

            migrationBuilder.DropColumn(
                name: "GrupoEquipamento",
                table: "Equipamentos");
        }
    }
}
