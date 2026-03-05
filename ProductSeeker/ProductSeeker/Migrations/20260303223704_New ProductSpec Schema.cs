using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductSeeker.Migrations
{
    /// <inheritdoc />
    public partial class NewProductSpecSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecAttributeValue_ProductSpecs_ProductSpecModelId",
                table: "ProductSpecAttributeValue");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecAttributeValue_ProductSpecModelId",
                table: "ProductSpecAttributeValue");

            migrationBuilder.DropColumn(
                name: "ProductSpecModelId",
                table: "ProductSpecAttributeValue");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "ProductSpecs",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "NetContent",
                table: "ProductSpecs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TACC",
                table: "ProductSpecs",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasure",
                table: "ProductSpecs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecAttributeValue_ProductSpecId",
                table: "ProductSpecAttributeValue",
                column: "ProductSpecId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecAttributeValue_ProductSpecs_ProductSpecId",
                table: "ProductSpecAttributeValue",
                column: "ProductSpecId",
                principalTable: "ProductSpecs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecAttributeValue_ProductSpecs_ProductSpecId",
                table: "ProductSpecAttributeValue");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecAttributeValue_ProductSpecId",
                table: "ProductSpecAttributeValue");

            migrationBuilder.DropColumn(
                name: "NetContent",
                table: "ProductSpecs");

            migrationBuilder.DropColumn(
                name: "TACC",
                table: "ProductSpecs");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasure",
                table: "ProductSpecs");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "ProductSpecs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);

            migrationBuilder.AddColumn<int>(
                name: "ProductSpecModelId",
                table: "ProductSpecAttributeValue",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecAttributeValue_ProductSpecModelId",
                table: "ProductSpecAttributeValue",
                column: "ProductSpecModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecAttributeValue_ProductSpecs_ProductSpecModelId",
                table: "ProductSpecAttributeValue",
                column: "ProductSpecModelId",
                principalTable: "ProductSpecs",
                principalColumn: "Id");
        }
    }
}
