using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMvcFull.Migrations
{
    /// <inheritdoc />
    public partial class EditModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loaicaosu",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "loaicaosu1",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "loaicaosu2",
                table: "Products",
                newName: "chieudailoithep");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "chieudailoithep",
                table: "Products",
                newName: "loaicaosu2");

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
        }
    }
}
