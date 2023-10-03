using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCartEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ProductItems_ProductId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Users_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductConfigurations",
                table: "ProductConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_ProductConfigurations_ProductItemId",
                table: "ProductConfigurations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ShoppingCartItems",
                newName: "ProductItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_ProductItemId");

            migrationBuilder.AddColumn<long>(
                name: "CartId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ProductConfigurations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductConfigurations",
                table: "ProductConfigurations",
                columns: new[] { "ProductItemId", "VariationOptionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CartId",
                table: "Users",
                column: "CartId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ProductItems_ProductItemId",
                table: "ShoppingCartItems",
                column: "ProductItemId",
                principalTable: "ProductItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ShoppingCarts_CartId",
                table: "Users",
                column: "CartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ProductItems_ProductItemId",
                table: "ShoppingCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_ShoppingCarts_CartId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CartId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductConfigurations",
                table: "ProductConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParentId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ProductItemId",
                table: "ShoppingCartItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_ProductItemId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_ProductId");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "ShoppingCarts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "ProductConfigurations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductConfigurations",
                table: "ProductConfigurations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductConfigurations_ProductItemId",
                table: "ProductConfigurations",
                column: "ProductItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ProductItems_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId",
                principalTable: "ProductItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Users_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
