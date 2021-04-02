using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class add_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DateCreated", "IsDeleted", "Name", "Description" },
                values: new Object[] { Guid.NewGuid(), DateTime.Now, false, "Hat", "Category representing a hat" });
            
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DateCreated", "IsDeleted", "Name", "Description" },
                values: new Object[] { Guid.NewGuid(), DateTime.Now, false, "Top", "Category representing a top" });
            
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DateCreated", "IsDeleted", "Name", "Description" },
                values: new Object[] { Guid.NewGuid(), DateTime.Now, false, "Bottom", "Category representing a bottom" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        
        }
    }
}