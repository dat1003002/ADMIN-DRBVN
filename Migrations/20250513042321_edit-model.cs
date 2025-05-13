using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMvcFull.Migrations
{
    /// <inheritdoc />
    public partial class editmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "chieudailoithep",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "dodaycaosubo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "dodaycaosuketdinh3t",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "khoangcach2daumoinoiloithep",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "khocaosubo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "khocaosuketdinh3t",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "kholoithep",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "kichthuoccuacaosudanmoinoi",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "solink",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sosoiloithep",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tocdoquan",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "trongluongloithepspinning",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "chieudailoithep",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "dodaycaosubo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "dodaycaosuketdinh3t",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "khoangcach2daumoinoiloithep",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "khocaosubo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "khocaosuketdinh3t",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "kholoithep",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "kichthuoccuacaosudanmoinoi",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "solink",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "sosoiloithep",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "tocdoquan",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "trongluongloithepspinning",
                table: "Products");
        }
    }
}
