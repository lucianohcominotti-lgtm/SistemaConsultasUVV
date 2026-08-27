using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaConsultasUVV.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarStatusConsulta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Consultas",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Consultas");
        }
    }
}
