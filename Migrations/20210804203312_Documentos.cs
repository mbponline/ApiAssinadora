using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAssinadora.Migrations
{
    public partial class Documentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Caminho",
                table: "Documentos",
                newName: "UserId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Arquivo",
                table: "Documentos",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CertificadoId",
                table: "Documentos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Documentos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_CertificadoId",
                table: "Documentos",
                column: "CertificadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_UserId",
                table: "Documentos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_AspNetUsers_UserId",
                table: "Documentos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Certificados_CertificadoId",
                table: "Documentos",
                column: "CertificadoId",
                principalTable: "Certificados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_AspNetUsers_UserId",
                table: "Documentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Certificados_CertificadoId",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_CertificadoId",
                table: "Documentos");

            migrationBuilder.DropIndex(
                name: "IX_Documentos_UserId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Arquivo",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "CertificadoId",
                table: "Documentos");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Documentos");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Documentos",
                newName: "Caminho");
        }
    }
}
