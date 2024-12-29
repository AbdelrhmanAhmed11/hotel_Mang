using System.ComponentModel.DataAnnotations;

namespace Hotel_Management_System.Models
{
    public class Hotel
    {
        [Key]
        public int HotelID {  get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        [Range(1, 5)]
        public int HotelRating { get; set; }

        public ICollection<Room> rooms {  get; set; }
    }
}
