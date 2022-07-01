using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestDDDApi.Api.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CustomerID", x => x.customerID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductID", x => x.productID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    orderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OrderID", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "customerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    orderItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    orderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OrderItemID", x => x.orderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_orderID",
                        column: x => x.orderID,
                        principalTable: "Orders",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_orderID",
                table: "OrderItems",
                column: "orderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
