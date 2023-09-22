using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Addtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_Attachments_AttachmentId",
                table: "ProductItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Attachments_AttachmentId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_AttachmentId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_AttachmentId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "ProductItems");

            migrationBuilder.CreateTable(
                name: "ProductAttachments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    AttachmentId = table.Column<long>(type: "bigint", nullable: false),
                    CretedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatetAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttachments_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttachments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductItemAttachments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductItemId = table.Column<long>(type: "bigint", nullable: false),
                    AttachmentId = table.Column<long>(type: "bigint", nullable: false),
                    CretedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatetAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItemAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductItemAttachments_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductItemAttachments_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttachments_AttachmentId",
                table: "ProductAttachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttachments_ProductId",
                table: "ProductAttachments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItemAttachments_AttachmentId",
                table: "ProductItemAttachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItemAttachments_ProductItemId",
                table: "ProductItemAttachments",
                column: "ProductItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttachments");

            migrationBuilder.DropTable(
                name: "ProductItemAttachments");

            migrationBuilder.AddColumn<long>(
                name: "AttachmentId",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AttachmentId",
                table: "ProductItems",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AttachmentId",
                table: "Products",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_AttachmentId",
                table: "ProductItems",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_Attachments_AttachmentId",
                table: "ProductItems",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Attachments_AttachmentId",
                table: "Products",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
