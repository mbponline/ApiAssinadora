using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAssinadora.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Arquivo",
                table: "Documentos",
                type: "longblob",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "blob",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Arquivo",
                table: "Certificados",
                type: "longblob",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "blob",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Arquivo",
                table: "Documentos",
                type: "blob",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "longblob",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Arquivo",
                table: "Certificados",
                type: "blob",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "longblob",
                oldNullable: true);
        }
    }
}
