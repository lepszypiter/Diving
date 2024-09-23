using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diving.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Small : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ClientCourses",
            table: "Clients");

        migrationBuilder.DropColumn(
            name: "License",
            table: "Clients");

        migrationBuilder.AlterColumn<string>(
            name: "Surname",
            table: "Clients",
            type: "TEXT",
            nullable: false,
            defaultValue: string.Empty,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Clients",
            type: "TEXT",
            nullable: false,
            defaultValue: string.Empty,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "Clients",
            type: "TEXT",
            nullable: false,
            defaultValue: string.Empty,
            oldClrType: typeof(string),
            oldType: "TEXT",
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Surname",
            table: "Clients",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "Name",
            table: "Clients",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AlterColumn<string>(
            name: "Email",
            table: "Clients",
            type: "TEXT",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "TEXT");

        migrationBuilder.AddColumn<string>(
            name: "ClientCourses",
            table: "Clients",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "License",
            table: "Clients",
            type: "TEXT",
            nullable: true);
    }
}
