using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "RoomViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerNight = table.Column<double>(type: "float", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "hotel",
                columns: table => new
                {
                    HotelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelRating = table.Column<int>(type: "int", nullable: false),
                    RoomViewModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotel", x => x.HotelID);
                    table.ForeignKey(
                        name: "FK_hotel_RoomViewModel_RoomViewModelId",
                        column: x => x.RoomViewModelId,
                        principalTable: "RoomViewModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerNight = table.Column<double>(type: "float", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    RoomViewModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_room_RoomViewModel_RoomViewModelId",
                        column: x => x.RoomViewModelId,
                        principalTable: "RoomViewModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_room_hotel_HotelID",
                        column: x => x.HotelID,
                        principalTable: "hotel",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationTotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_reservation_customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservation_room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hotel_RoomViewModelId",
                table: "hotel",
                column: "RoomViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_CustomerID",
                table: "reservation",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_RoomID",
                table: "reservation",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_room_HotelID",
                table: "room",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_room_RoomViewModelId",
                table: "room",
                column: "RoomViewModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservation");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "room");

            migrationBuilder.DropTable(
                name: "hotel");

            migrationBuilder.DropTable(
                name: "RoomViewModel");
        }
    }
}
