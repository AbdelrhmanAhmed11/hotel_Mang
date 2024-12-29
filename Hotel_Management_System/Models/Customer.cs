using System.ComponentModel.DataAnnotations;

namespace Hotel_Management_System.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID {  get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone {  get; set; }
        
        public ICollection<Reservation> reservations { get; set; }
    }
}
