using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomSearchController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomSearchController(HotelDbContext context)
        {
            _context = context;
        }

        [HttpGet("{hotelName}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByHotel(string hotelName)
        {
            try
            {
                var hotel = await _context.Hotels
                    .Include(h => h.Rooms)
                    .FirstOrDefaultAsync(h => h.Name == hotelName);

                if (hotel == null)
                {
                    return NotFound("Hotel not found.");
                }

                return Ok(hotel.Rooms);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, "An error occurred while retrieving rooms.");
            }
        }

        [HttpGet("location/{location}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByLocation(string location)
        {
            try
            {
                var hotels = await _context.Hotels
                    .Include(h => h.Rooms)
                    .Where(h => h.Location == location)
                    .ToListAsync();

                if (hotels == null || hotels.Count == 0)
                {
                    return NotFound("No hotels found in the specified location.");
                }

                var rooms = hotels.SelectMany(h => h.Rooms).ToList();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, "An error occurred while retrieving rooms.");
            }
        }
    }

}
