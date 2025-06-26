using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusGym.Migrations
{
    /// <inheritdoc />
    public partial class fichaDeTreinoTipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoTreino",
                table: "FichasDeTreino",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoTreino",
                table: "FichasDeTreino");
        }
    }
}
