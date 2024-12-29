using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Management_System.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public int HotelID {  get; set; }
        [ForeignKey("HotelID")]
        public Hotel hotel { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public double PricePerNight {  get; set; }
        public bool IsAvailable { get; set; }
        
        public ICollection<Reservation> reservations { get; set; }
    }
}
