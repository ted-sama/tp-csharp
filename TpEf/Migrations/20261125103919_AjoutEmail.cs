using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TpEf.Migrations
{
    /// <inheritdoc />
    public partial class AjoutEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Etudiants",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Etudiants");
        }
    }
}
