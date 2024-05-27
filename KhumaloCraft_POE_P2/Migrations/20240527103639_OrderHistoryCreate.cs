using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KhumaloCraft_POE_P2.Migrations
{
    /// <inheritdoc />
    public partial class OrderHistoryCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Order_Product_ProductId",
            //    table: "Order");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Order_AspNetUsers_UserId",
            //    table: "Order");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Order",
            //    table: "Order");

            //migrationBuilder.RenameTable(
            //    name: "Order",
            //    newName: "Orders");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Order_UserId",
            //    table: "Orders",
            //    newName: "IX_Orders_UserId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Order_ProductId",
            //    table: "Orders",
            //    newName: "IX_Orders_ProductId");

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "Date",
            //    table: "Orders",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "ModifiedDate",
            //    table: "Orders",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Orders",
            //    table: "Orders",
            //    column: "OrderId");

            migrationBuilder.CreateTable(
                name: "OrderHistories",
                columns: table => new
                {
                    OrderHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistories", x => x.OrderHistoryId);
                    table.ForeignKey(
                        name: "FK_OrderHistories_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories_OrderHistoryId",
                table: "OrderHistories",
                column: "OrderHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistories",
                table: "OrderHistories",
                column: "OrderHistoryId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_OrderHistories_OrderId",
            //    table: "OrderHistories",
            //    column: "OrderId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders_Product_ProductId",
            //    table: "Orders",
            //    column: "ProductId",
            //    principalTable: "Product",
            //    principalColumn: "ProductId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Orders_AspNetUsers_UserId",
            //    table: "Orders",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id");
        }
    

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "OrderHistories");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_Product_ProductId",
            //    table: "Orders");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Orders_AspNetUsers_UserId",
            //    table: "Orders");

            //migrationBuilder.DropTable(
            //    name: "OrderHistories");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Orders",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "Date",
            //    table: "Orders");

            //migrationBuilder.DropColumn(
            //    name: "ModifiedDate",
            //    table: "Orders");

            //migrationBuilder.RenameTable(
            //    name: "Orders",
            //    newName: "Order");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Orders_UserId",
            //    table: "Order",
            //    newName: "IX_Order_UserId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Orders_ProductId",
            //    table: "Order",
            //    newName: "IX_Order_ProductId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Order",
            //    table: "Order",
            //    column: "OrderId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Order_Product_ProductId",
            //    table: "Order",
            //    column: "ProductId",
            //    principalTable: "Product",
            //    principalColumn: "ProductId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Order_AspNetUsers_UserId",
            //    table: "Order",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id");
        }
    }
}
