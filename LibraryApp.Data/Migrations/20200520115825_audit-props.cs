using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApp.Data.Migrations
{
    public partial class auditprops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Books",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Books",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Authors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Authors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Articles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Articles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("67cf18b2-4ce2-489b-99c4-a24ab3eb6d16"),
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2020, 5, 20, 14, 58, 25, 82, DateTimeKind.Local).AddTicks(5744), new DateTime(2020, 5, 20, 14, 58, 25, 82, DateTimeKind.Local).AddTicks(5779) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7e121dc2-6a56-4324-9f02-d5570f210824"),
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTime(2020, 5, 20, 14, 58, 25, 84, DateTimeKind.Local).AddTicks(7951), new DateTime(2020, 5, 20, 14, 58, 25, 84, DateTimeKind.Local).AddTicks(7985) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Articles");
        }
    }
}
