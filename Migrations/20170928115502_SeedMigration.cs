using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace backend.Migrations
{
    public partial class SeedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO messages(Owner) VALUES ('masoud')");
            migrationBuilder.Sql("INSERT INTO messages(Owner) VALUES ('mahmoud')");

            migrationBuilder.Sql("INSERT INTO messages(text) VALUES ('fine')");
            migrationBuilder.Sql("INSERT INTO messages(text) VALUES ('excellent')");
 

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
