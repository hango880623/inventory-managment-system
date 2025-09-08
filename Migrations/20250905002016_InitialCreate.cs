using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InventoryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCustomers",
                columns: table => new
                {
                    customerid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customername = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    zipcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCustomers", x => x.customerid);
                });

            migrationBuilder.CreateTable(
                name: "tblProducts",
                columns: table => new
                {
                    productid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    purchaseprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    salesprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    instock = table.Column<int>(type: "int", nullable: true),
                    valuehand = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProducts", x => x.productid);
                });

            migrationBuilder.CreateTable(
                name: "tblRoles",
                columns: table => new
                {
                    roleid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoles", x => x.roleid);
                });

            migrationBuilder.CreateTable(
                name: "tblSuppliers",
                columns: table => new
                {
                    supplierid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    suppliername = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    city = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    zipcode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSuppliers", x => x.supplierid);
                });

            migrationBuilder.CreateTable(
                name: "tblSalesOrders",
                columns: table => new
                {
                    salesorderid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salesorderno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    customerid = table.Column<int>(type: "int", nullable: false),
                    orderdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSalesOrders", x => x.salesorderid);
                    table.ForeignKey(
                        name: "FK_tblSalesOrders_tblCustomers_customerid",
                        column: x => x.customerid,
                        principalTable: "tblCustomers",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    roleid = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    accountstatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    resettoken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.userid);
                    table.ForeignKey(
                        name: "FK_tblUsers_tblRoles_roleid",
                        column: x => x.roleid,
                        principalTable: "tblRoles",
                        principalColumn: "roleid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPurchaseOrders",
                columns: table => new
                {
                    purchaseorderid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purchaseorderno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    supplierid = table.Column<int>(type: "int", nullable: false),
                    purchasedate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPurchaseOrders", x => x.purchaseorderid);
                    table.ForeignKey(
                        name: "FK_tblPurchaseOrders_tblSuppliers_supplierid",
                        column: x => x.supplierid,
                        principalTable: "tblSuppliers",
                        principalColumn: "supplierid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSalesOrderDetails",
                columns: table => new
                {
                    salesorderdetailsid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salesorderid = table.Column<int>(type: "int", nullable: false),
                    productid = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    qty = table.Column<int>(type: "int", nullable: true),
                    salesprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSalesOrderDetails", x => x.salesorderdetailsid);
                    table.ForeignKey(
                        name: "FK_tblSalesOrderDetails_tblProducts_productid",
                        column: x => x.productid,
                        principalTable: "tblProducts",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSalesOrderDetails_tblSalesOrders_salesorderid",
                        column: x => x.salesorderid,
                        principalTable: "tblSalesOrders",
                        principalColumn: "salesorderid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPurchaseOrderDetails",
                columns: table => new
                {
                    purchaseorderdetailsid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purchaseorderid = table.Column<int>(type: "int", nullable: false),
                    productid = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    qty = table.Column<int>(type: "int", nullable: true),
                    salesprice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPurchaseOrderDetails", x => x.purchaseorderdetailsid);
                    table.ForeignKey(
                        name: "FK_tblPurchaseOrderDetails_tblProducts_productid",
                        column: x => x.productid,
                        principalTable: "tblProducts",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPurchaseOrderDetails_tblPurchaseOrders_purchaseorderid",
                        column: x => x.purchaseorderid,
                        principalTable: "tblPurchaseOrders",
                        principalColumn: "purchaseorderid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tblRoles",
                columns: new[] { "roleid", "description", "role" },
                values: new object[,]
                {
                    { 1, "System Administrator", "Admin" },
                    { 2, "Inventory Manager", "Manager" },
                    { 3, "Regular User", "User" }
                });

            migrationBuilder.InsertData(
                table: "tblUsers",
                columns: new[] { "userid", "accountstatus", "email", "name", "password", "photo", "resettoken", "roleid" },
                values: new object[] { 1, "Active", "admin@inventory.com", "System Administrator", "$2a$11$3kTqe33jNG0Rw0gHc0JJpOoXA/UQzmX7S8U2CqdLMJyxBSGyKgBZK", null, null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_tblPurchaseOrderDetails_productid",
                table: "tblPurchaseOrderDetails",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_tblPurchaseOrderDetails_purchaseorderid",
                table: "tblPurchaseOrderDetails",
                column: "purchaseorderid");

            migrationBuilder.CreateIndex(
                name: "IX_tblPurchaseOrders_supplierid",
                table: "tblPurchaseOrders",
                column: "supplierid");

            migrationBuilder.CreateIndex(
                name: "IX_tblSalesOrderDetails_productid",
                table: "tblSalesOrderDetails",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_tblSalesOrderDetails_salesorderid",
                table: "tblSalesOrderDetails",
                column: "salesorderid");

            migrationBuilder.CreateIndex(
                name: "IX_tblSalesOrders_customerid",
                table: "tblSalesOrders",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_tblUsers_roleid",
                table: "tblUsers",
                column: "roleid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "tblSalesOrderDetails");

            migrationBuilder.DropTable(
                name: "tblUsers");

            migrationBuilder.DropTable(
                name: "tblPurchaseOrders");

            migrationBuilder.DropTable(
                name: "tblProducts");

            migrationBuilder.DropTable(
                name: "tblSalesOrders");

            migrationBuilder.DropTable(
                name: "tblRoles");

            migrationBuilder.DropTable(
                name: "tblSuppliers");

            migrationBuilder.DropTable(
                name: "tblCustomers");
        }
    }
}
