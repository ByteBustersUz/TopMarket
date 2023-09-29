using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Manytomanyconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromotionCategories",
                table: "PromotionCategories");

            migrationBuilder.DropIndex(
                name: "IX_PromotionCategories_PromotionId",
                table: "PromotionCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductConfigurations",
                table: "ProductConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_ProductConfigurations_ProductItemId",
                table: "ProductConfigurations");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UserAddresses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "PromotionCategories",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ProductConfigurations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                columns: new[] { "UserId", "AddressId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromotionCategories",
                table: "PromotionCategories",
                columns: new[] { "PromotionId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductConfigurations",
                table: "ProductConfigurations",
                columns: new[] { "ProductItemId", "VariationOptionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories",
                column: "ParentId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromotionCategories",
                table: "PromotionCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductConfigurations",
                table: "ProductConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentId",
                table: "Categories");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "UserAddresses",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "PromotionCategories",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ProductConfigurations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromotionCategories",
                table: "PromotionCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductConfigurations",
                table: "ProductConfigurations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_UserId",
                table: "UserAddresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCategories_PromotionId",
                table: "PromotionCategories",
                column: "PromotionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductConfigurations_ProductItemId",
                table: "ProductConfigurations",
                column: "ProductItemId");
        }
    }
}
