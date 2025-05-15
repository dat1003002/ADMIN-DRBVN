using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMvcFull.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "chieudai",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "chieudai1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "chieudai2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "doday1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "doday2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "kho",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "kho1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "kho2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "loaicaosu",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "loaicaosu1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "loaicaosu2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "loaikhuondun",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "loaikhuondun1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "loaikhuondun2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "tocdomotor",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "tocdomotor1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "tocdomotor2",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "trongluong",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "trongluong1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "trongluong2",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "chieudai",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "chieudai1",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "chieudai2",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "doday1",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "doday2",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "kho",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "kho1",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "kho2",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loaicaosu",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loaicaosu1",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loaicaosu2",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loaikhuondun",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loaikhuondun1",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loaikhuondun2",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tocdomotor",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tocdomotor1",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tocdomotor2",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "trongluong",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "trongluong1",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "trongluong2",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
