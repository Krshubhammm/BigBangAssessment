using HotelBookingSystem.Models;
using HotelBookingSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Controllers
{
    [Authorize] // Apply authorization to all actions in this controller
    [Route("api/[controller]")]
        [ApiController]
        public class HotelsController : ControllerBase
        {
            private readonly HotelDbContext _context;

            public HotelsController(HotelDbContext context)
            {
                _context = context;
            }

        // GET: api/hotels
     //   [AllowAnonymous] // Allow anonymous access to retrieve all hotels
        [HttpGet]
            public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
            {
                try
                {
                    // Retrieve all hotels from the database
                    var hotels = await _context.Hotels.Include(h => h.Rooms).ToListAsync();
                    return Ok(hotels);
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return appropriate error response
                    return StatusCode(500, "An error occurred while retrieving hotels.");
                }
            }

            // GET: api/hotels/{id}
            [HttpGet("{id}")]
            public async Task<ActionResult<Hotel>> GetHotel(int id)
            {
                try
                {
                    // Find the hotel with the specified id
                    var hotel = await _context.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.Id == id);

                    if (hotel == null)
                    {
                        return NotFound();
                    }

                    return Ok(hotel);
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return appropriate error response
                    return StatusCode(500, "An error occurred while retrieving the hotel.");
                }
            }

            // POST: api/hotels
            [HttpPost]
            public async Task<ActionResult<Hotel>> CreateHotel(Hotel hotel)
            {
                try
                {
                    // Add the new hotel to the database
                    _context.Hotels.Add(hotel);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return appropriate error response
                    return StatusCode(500, "An error occurred while creating the hotel.");
                }
            }

        // PUT: api/hotels/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, Hotel hotel)
        {
            try
            {
                if (id != hotel.Id)
                {
                    return BadRequest();
                }

                // Update the hotel in the database
                _context.Entry(hotel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Reload the updated hotel from the database
                await _context.Entry(hotel).ReloadAsync();

                return Ok(hotel); // Return the updated hotel in the response
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, "An error occurred while updating the hotel.");
            }
        }


        // DELETE: api/hotels/{id}
        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteHotel(int id)
        {
           // try
            //{
                // Find the hotel with the specified id
                var hotel = await _context.Hotels.FindAsync(id);
                if (hotel == null)
                {
                    return NotFound();
                }

                // Delete the hotel from the database
                _context.Hotels.Remove(hotel);
                await _context.SaveChangesAsync();

                return Ok(hotel); // Return the deleted hotel in the response
           // }
         /* //  catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, "An error occurred while deleting the hotel.");
            }*/
        }

    }
}
