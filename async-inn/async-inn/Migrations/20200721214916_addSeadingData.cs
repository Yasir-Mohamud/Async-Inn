using Microsoft.EntityFrameworkCore.Migrations;

namespace async_inn.Migrations
{
    public partial class addSeadingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fridge" },
                    { 2, "Water Fountain" },
                    { 3, "Pool" }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "City", "Name", "Phone", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "Los Angeles", "Y.M Hotels", "123-456-789", "California", "5th and Oliver street" },
                    { 2, "Seattle", "Y.M Luxury", "222-456-789", "Washington", "100th south ave" },
                    { 3, "Portland", "Y.M Complex", "123-654-789", "Oregon", "23rd Jump street" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Layout", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Amaterasu" },
                    { 2, 2, "Ying Yang" },
                    { 3, 3, "Habibi" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Amenities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
