using Hotel_Management_System.Models;
using Humanizer;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Management_System.ViewModels
{
    public class RoomViewModel
    {
        [Key]
        public int Id { get; set; }
        public int HotelID {  get; set; }
        public int RoomID {  get; set; }
        public int RoomNumber {  get; set; }
        public string RoomType { get; set; }
        public Double PricePerNight { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<Hotel> hotels { get; set; }
        public ICollection<Room> rooms { get; set; }
    }
}
