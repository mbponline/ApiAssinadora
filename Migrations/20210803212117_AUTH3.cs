using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAssinadora.Migrations
{
    public partial class AUTH3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Certificados",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "teste",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Certificados");

            migrationBuilder.DropColumn(
                name: "teste",
                table: "AspNetUsers");
        }
    }
}
