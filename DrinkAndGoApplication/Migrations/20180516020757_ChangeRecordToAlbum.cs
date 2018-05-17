using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AlbumStore.Migrations
{
    public partial class ChangeRecordToAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlbumName",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "LongDescription",
                table: "Albums",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AlbumLabel",
                table: "Albums",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlbumName",
                table: "OrderDetails",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LongDescription",
                table: "Albums",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "AlbumLabel",
                table: "Albums",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
