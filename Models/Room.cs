using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        public string?  Name { get; set; }
        public int Price { get; set; }

        // Add any additional properties as needed
        public bool IsOccupied { get; set; }
        // Define a reference to the hotel that owns the room

        public Hotel? Hotel { get; set; }
         public ICollection<Reservation>? Reservations { get; set; }
    }
}
