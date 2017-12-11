using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DVDManagement.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    account = table.Column<string>(maxLength: 10, nullable: false),
                    email = table.Column<string>(maxLength: 254, nullable: false),
                    name = table.Column<string>(maxLength: 15, nullable: false),
                    password = table.Column<string>(maxLength: 20, nullable: false),
                    permission = table.Column<string>(maxLength: 20, nullable: false),
                    phone = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.account);
                });

            migrationBuilder.CreateTable(
                name: "Dvd_info",
                columns: table => new
                {
                    movie_code = table.Column<string>(maxLength: 10, nullable: false),
                    create_date = table.Column<DateTime>(nullable: false),
                    name = table.Column<string>(maxLength: 15, nullable: false),
                    overdue = table.Column<int>(nullable: false),
                    rent = table.Column<int>(nullable: false),
                    time_limit = table.Column<byte>(nullable: false),
                    type = table.Column<byte>(nullable: false),
                    update_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dvd_info", x => x.movie_code);
                });

            migrationBuilder.CreateTable(
                name: "Dvd_recode",
                columns: table => new
                {
                    movie_code = table.Column<string>(maxLength: 10, nullable: false),
                    type = table.Column<byte>(nullable: false),
                    user_no = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dvd_recode", x => x.movie_code);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    user_no = table.Column<string>(maxLength: 10, nullable: false),
                    add_amount = table.Column<int>(nullable: false),
                    movie_code = table.Column<string>(maxLength: 10, nullable: true),
                    trans_date = table.Column<DateTime>(nullable: false),
                    type = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.user_no);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_no = table.Column<string>(maxLength: 10, nullable: false),
                    birthday = table.Column<byte>(nullable: false),
                    email = table.Column<string>(maxLength: 254, nullable: false),
                    name = table.Column<string>(maxLength: 15, nullable: false),
                    phone_1 = table.Column<string>(maxLength: 10, nullable: false),
                    phone_2 = table.Column<string>(maxLength: 10, nullable: true),
                    sex = table.Column<bool>(nullable: false),
                    store_amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_no);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Dvd_info");

            migrationBuilder.DropTable(
                name: "Dvd_recode");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
