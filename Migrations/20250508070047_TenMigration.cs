using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMvcFull.Migrations
{
    /// <inheritdoc />
    public partial class TenMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "aplucdaudunloithep",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "caosubo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "caosuketdinh",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "caosur514",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

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
                name: "khoangcach2daumoinoibo",
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
                name: "loithepsaukhidun",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loitheptruockhidun",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nhietdodaumaydun",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nhietdotrucxoan",
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
                name: "tocdocolingdrum",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tocdoduncaosu",
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
                name: "aplucdaudunloithep",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "caosubo",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "caosuketdinh",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "caosur514",
                table: "Products");

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
                name: "khoangcach2daumoinoibo",
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
                name: "loithepsaukhidun",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "loitheptruockhidun",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "nhietdodaumaydun",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "nhietdotrucxoan",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "solink",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "sosoiloithep",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "tocdocolingdrum",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "tocdoduncaosu",
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
