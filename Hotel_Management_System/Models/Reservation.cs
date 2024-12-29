using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Management_System.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }
        public int RoomID {  get; set; }
        [ForeignKey("RoomID")]
        public Room room { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer customer { get; set; }
        public DateTime CheckInDate {  get; set; }
        public DateTime CheckOutDate { get; set; }
        public double ReservationTotalPrice {  get; set; }
    }
}
