using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspnetCoreMvcFull.Migrations
{
    /// <inheritdoc />
    public partial class addmodelloaicaosu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "loaicaosu",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loaicaosu",
                table: "Products");
        }
    }
}
