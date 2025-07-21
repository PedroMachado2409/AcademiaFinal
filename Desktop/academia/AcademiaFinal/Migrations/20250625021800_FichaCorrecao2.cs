using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusGym.Migrations
{
    /// <inheritdoc />
    public partial class FichaCorrecao2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_ItensFichaDeTreino_ItemFichaDeTreinoId",
                table: "Equipamentos");

            migrationBuilder.DropIndex(
                name: "IX_Equipamentos_ItemFichaDeTreinoId",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "ItemFichaDeTreinoId",
                table: "Equipamentos");

            migrationBuilder.AddColumn<int>(
                name: "EquipamentoId",
                table: "ItensFichaDeTreino",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItensFichaDeTreino_EquipamentoId",
                table: "ItensFichaDeTreino",
                column: "EquipamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensFichaDeTreino_Equipamentos_EquipamentoId",
                table: "ItensFichaDeTreino",
                column: "EquipamentoId",
                principalTable: "Equipamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensFichaDeTreino_Equipamentos_EquipamentoId",
                table: "ItensFichaDeTreino");

            migrationBuilder.DropIndex(
                name: "IX_ItensFichaDeTreino_EquipamentoId",
                table: "ItensFichaDeTreino");

            migrationBuilder.DropColumn(
                name: "EquipamentoId",
                table: "ItensFichaDeTreino");

            migrationBuilder.AddColumn<int>(
                name: "ItemFichaDeTreinoId",
                table: "Equipamentos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_ItemFichaDeTreinoId",
                table: "Equipamentos",
                column: "ItemFichaDeTreinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamentos_ItensFichaDeTreino_ItemFichaDeTreinoId",
                table: "Equipamentos",
                column: "ItemFichaDeTreinoId",
                principalTable: "ItensFichaDeTreino",
                principalColumn: "Id");
        }
    }
}
