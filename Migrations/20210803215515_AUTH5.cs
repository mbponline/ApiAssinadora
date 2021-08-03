using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAssinadora.Migrations
{
    public partial class AUTH5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Certificados_UserId",
                table: "Certificados",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificados_AspNetUsers_UserId",
                table: "Certificados",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificados_AspNetUsers_UserId",
                table: "Certificados");

            migrationBuilder.DropIndex(
                name: "IX_Certificados_UserId",
                table: "Certificados");
        }
    }
}
