using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clgproject.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cust_Transactions",
                columns: table => new
                {
                    t_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    t_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    t_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cust_Transactions", x => x.t_id);
                });

            migrationBuilder.CreateTable(
                name: "MainTransactions",
                columns: table => new
                {
                    t_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    t_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    t_status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainTransactions", x => x.t_id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contact = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cust_Transaction_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cust_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    account_no = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customer_id);
                    table.ForeignKey(
                        name: "FK_Customers_cust_Transactions_Cust_Transaction_id",
                        column: x => x.Cust_Transaction_id,
                        principalTable: "cust_Transactions",
                        principalColumn: "t_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MainSuppliers",
                columns: table => new
                {
                    mainsupp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mainsupp_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mainsupp_contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mainsupp_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainTransactionId = table.Column<int>(type: "int", nullable: false),
                    account_no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainSuppliers", x => x.mainsupp_id);
                    table.ForeignKey(
                        name: "FK_MainSuppliers_MainTransactions_MainTransactionId",
                        column: x => x.MainTransactionId,
                        principalTable: "MainTransactions",
                        principalColumn: "t_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "C_orders",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supplier_id = table.Column<int>(type: "int", nullable: false),
                    date_of_order = table.Column<DateTime>(type: "datetime2", nullable: false),
                    order_details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<float>(type: "real", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    o_address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C_orders", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_C_orders_Suppliers_Supplier_id",
                        column: x => x.Supplier_id,
                        principalTable: "Suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mainsupp_id = table.Column<int>(type: "int", nullable: false),
                    date_of_order = table.Column<DateTime>(type: "datetime2", nullable: false),
                    order_details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<float>(type: "real", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    o_address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_Products_MainSuppliers_Mainsupp_id",
                        column: x => x.Mainsupp_id,
                        principalTable: "MainSuppliers",
                        principalColumn: "mainsupp_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "receiveCOrders",
                columns: table => new
                {
                    receive_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_id = table.Column<int>(type: "int", nullable: false),
                    receive_datetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receiveCOrders", x => x.receive_id);
                    table.ForeignKey(
                        name: "FK_receiveCOrders_C_orders_Order_id",
                        column: x => x.Order_id,
                        principalTable: "C_orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "returnCOrders",
                columns: table => new
                {
                    return_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_id = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_of_order = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_of_return = table.Column<DateTime>(type: "datetime2", nullable: false),
                    t_return = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_returnCOrders", x => x.return_id);
                    table.ForeignKey(
                        name: "FK_returnCOrders_C_orders_Order_id",
                        column: x => x.Order_id,
                        principalTable: "C_orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "receiveIOrders",
                columns: table => new
                {
                    receive_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_id = table.Column<int>(type: "int", nullable: false),
                    receive_datetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receiveIOrders", x => x.receive_id);
                    table.ForeignKey(
                        name: "FK_receiveIOrders_Products_Order_id",
                        column: x => x.Order_id,
                        principalTable: "Products",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "returnIOrders",
                columns: table => new
                {
                    return_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_id = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_of_order = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_of_return = table.Column<DateTime>(type: "datetime2", nullable: false),
                    t_return = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_returnIOrders", x => x.return_id);
                    table.ForeignKey(
                        name: "FK_returnIOrders_Products_Order_id",
                        column: x => x.Order_id,
                        principalTable: "Products",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_C_orders_Supplier_id",
                table: "C_orders",
                column: "Supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Cust_Transaction_id",
                table: "Customers",
                column: "Cust_Transaction_id");

            migrationBuilder.CreateIndex(
                name: "IX_MainSuppliers_MainTransactionId",
                table: "MainSuppliers",
                column: "MainTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Mainsupp_id",
                table: "Products",
                column: "Mainsupp_id");

            migrationBuilder.CreateIndex(
                name: "IX_receiveCOrders_Order_id",
                table: "receiveCOrders",
                column: "Order_id");

            migrationBuilder.CreateIndex(
                name: "IX_receiveIOrders_Order_id",
                table: "receiveIOrders",
                column: "Order_id");

            migrationBuilder.CreateIndex(
                name: "IX_returnCOrders_Order_id",
                table: "returnCOrders",
                column: "Order_id");

            migrationBuilder.CreateIndex(
                name: "IX_returnIOrders_Order_id",
                table: "returnIOrders",
                column: "Order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "receiveCOrders");

            migrationBuilder.DropTable(
                name: "receiveIOrders");

            migrationBuilder.DropTable(
                name: "returnCOrders");

            migrationBuilder.DropTable(
                name: "returnIOrders");

            migrationBuilder.DropTable(
                name: "cust_Transactions");

            migrationBuilder.DropTable(
                name: "C_orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "MainSuppliers");

            migrationBuilder.DropTable(
                name: "MainTransactions");
        }
    }
}
