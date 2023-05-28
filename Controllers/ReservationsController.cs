using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HotelBookingSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
        [ApiController]
        public class ReservationsController : ControllerBase
        {
            private readonly HotelDbContext _context;

            public ReservationsController(HotelDbContext context)
            {
                _context = context;
            }

            // GET: api/reservations
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
            {
                try
                {
                    // Retrieve all reservations from the database
                    var reservations = await _context.Reservations.Include(x => x.Hotel).ToListAsync();
                    return Ok(reservations);
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return appropriate error response
                    return StatusCode(500, "An error occurred while retrieving reservations.");
                }
            }

            // GET: api/reservations/{id}
            [HttpGet("{id}")]
            public async Task<ActionResult<Reservation>> GetReservation(int id)
            {
                try
                {
                    // Find the reservation with the specified id
                    var reservation = await _context.Reservations.FindAsync(id);

                    if (reservation == null)
                    {
                        return NotFound();
                    }

                    return Ok(reservation);
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return appropriate error response
                    return StatusCode(500, "An error occurred while retrieving the reservation.");
            }
        }

        // POST: api/reservations
        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
            {
                try
                {
                    // Add the new reservation to the database
                    _context.Reservations.Add(reservation);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return appropriate error response
                    return StatusCode(500, "An error occurred while creating the reservation.");
                }
            }

            // PUT: api/reservations/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateReservation(int id, Reservation reservation)
            {
                try
                {
                    if (id != reservation.Id)
                    {
                        return BadRequest();
                    }

                    // Update the reservation in the database
                    _context.Entry(reservation).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return NoContent();
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return appropriate error response
                    return StatusCode(500, "An error occurred while updating the reservation.");
                }
            }

            // DELETE: api/reservations/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteReservation(int id)
            {
                try
                {
                    // Find the reservation with the specified id
                    var reservation = await _context.Reservations.FindAsync(id);
                    if (reservation == null)
                    {
                        return NotFound();
                    }

                    // Delete the reservation from the database
                    _context.Reservations.Remove(reservation);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }
                catch (Exception ex)
                {
                    // Handle exceptions and return appropriate error response
                    return StatusCode(500, "An error occurred while deleting the reservation.");
                }
            }
        }
    }
