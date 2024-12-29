using Hotel_Management_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Management_System.ViewModels
{
    public class ReservationViewModel
    {
        [Key]
        public int Id { get; set; }
        public int RoomID { get; set; }
        public int CustomerID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public double ReservationTotalPrice { get; set; }

       
        public ICollection<Room> availablerooms {  get; set; }
        public ICollection<Customer> customers { get; set; }
    }
}
