using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Location { get; set; }

        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public string? Amenities { get; set; }

        public int StarRating { get; set; }

        // Define a collection of rooms associated with the hotel
        public ICollection<Room>? Rooms { get; set; }
    }
}
