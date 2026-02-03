using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusGym.Migrations
{
    /// <inheritdoc />
    public partial class FichaCorrecao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Repeticoes",
                table: "ItensFichaDeTreino",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Repeticoes",
                table: "ItensFichaDeTreino");
        }
    }
}
