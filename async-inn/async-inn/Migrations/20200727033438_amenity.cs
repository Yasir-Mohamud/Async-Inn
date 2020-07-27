using Microsoft.EntityFrameworkCore.Migrations;

namespace async_inn.Migrations
{
    public partial class amenity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "roomId",
                table: "Amenities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_roomId",
                table: "Amenities",
                column: "roomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Rooms_roomId",
                table: "Amenities",
                column: "roomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Rooms_roomId",
                table: "Amenities");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_roomId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "roomId",
                table: "Amenities");
        }
    }
}
