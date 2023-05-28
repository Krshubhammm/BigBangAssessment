using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        // Add any additional properties as needed
        public string? GuestName { get; set; }
        public int NumberOfGuests { get; set; }
        // Define relationships with other models
        public Hotel? Hotel { get; set; }
        public Room? Room { get; set; }

    }
}
