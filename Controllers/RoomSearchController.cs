using HotelBookingSystem.Models;
using HotelBookingSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HotelBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomSearchController : ControllerBase
    {
        private readonly HotelDbContext _context;
        private readonly IFilterAndCountRepository _filterAndCountRepository;

        public RoomSearchController(HotelDbContext context, IFilterAndCountRepository filterAndCountRepository)
        {
            _context = context;
            _filterAndCountRepository = filterAndCountRepository;
        }

        [HttpGet("{hotelName}")]
        public IActionResult GetRoomsByHotel(string hotelName)
        {
            try
            {
                var hotel = _context.Hotels
                    .Include(h => h.Rooms)
                    .FirstOrDefault(h => h.Name == hotelName);

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
        public IActionResult GetRoomsByLocation(string location)
        {
            try
            {
                var hotels = _filterAndCountRepository.GetHotelsByLocation(location);

                if (hotels == null || !hotels.Any())
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

        [HttpGet("count/{hotelId}")]
        public IActionResult GetRoomCount(int hotelId)
        {
            try
            {
                int roomCount = _filterAndCountRepository.GetRoomCount(hotelId);
                return Ok(roomCount);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, "An error occurred while retrieving room count.");
            }
        }

        [HttpGet("filter")]
        public IActionResult GetHotelsByFilter(string location, decimal? minPrice, decimal? maxPrice)
        {
            try
            {
                var hotels = _filterAndCountRepository.GetHotelsByFilter(location, minPrice, maxPrice);

                if (hotels == null || !hotels.Any())
                {
                    return NotFound("No hotels found based on the provided filter.");
                }

                return Ok(hotels);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, "An error occurred while retrieving hotels.");
            }
        }
    }
}
