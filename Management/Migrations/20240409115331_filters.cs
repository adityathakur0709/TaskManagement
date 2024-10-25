using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Management.Migrations
{
    /// <inheritdoc />
    public partial class filters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Priorities_PriorityId",
                table: "Activities");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityId",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityId",
                keyValue: "3");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Activities");

            migrationBuilder.AlterColumn<string>(
                name: "PriorityId",
                table: "Activities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { "home", "Home" },
                    { "office", "Office" },
                    { "onsite", "OnSite" }
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "PriorityId", "Name" },
                values: new object[,]
                {
                    { "high", "High" },
                    { "low", "Low" },
                    { "medium", "Medium" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Priorities_PriorityId",
                table: "Activities",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Priorities_PriorityId",
                table: "Activities");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "home");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "office");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "onsite");

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityId",
                keyValue: "high");

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityId",
                keyValue: "low");

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityId",
                keyValue: "medium");

            migrationBuilder.AlterColumn<string>(
                name: "PriorityId",
                table: "Activities",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { "1", "OnSite" },
                    { "2", "Office" },
                    { "3", "Home" }
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "PriorityId", "Name" },
                values: new object[,]
                {
                    { "1", "High" },
                    { "2", "Medium" },
                    { "3", "Low" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Priorities_PriorityId",
                table: "Activities",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "PriorityId");
        }
    }
}
