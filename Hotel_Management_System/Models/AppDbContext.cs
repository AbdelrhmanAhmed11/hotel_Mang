using Microsoft.EntityFrameworkCore;
using Hotel_Management_System.ViewModels;

namespace Hotel_Management_System.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> customer { get; set; }
        public DbSet<Hotel> hotel { get; set; }
        public DbSet<Reservation> reservation { get; set; }
        public DbSet<Room> room { get; set; }
        public DbSet<Hotel_Management_System.ViewModels.RoomViewModel> RoomViewModel { get; set; } = default!;
    }
}
