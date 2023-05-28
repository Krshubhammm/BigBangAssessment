using HotelBookingSystem.Models;
using HotelBookingSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingSystem.Controllers
{
    [Authorize] // Apply authorization to all actions in this controller
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/rooms
        [AllowAnonymous] // Allow anonymous access to retrieve all rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            try
            {
                // Retrieve all rooms from the database
                var rooms = await _context.Rooms.ToListAsync();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, "An error occurred while retrieving rooms.");
            }
        }

        // GET: api/rooms/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            try
            {
                // Find the room with the specified id
                var room = await _context.Rooms.FindAsync(id);

                if (room == null)
                {
                    return NotFound();
                }

                return Ok(room);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, "An error occurred while retrieving the room.");
            }
        }

        // POST: api/rooms
        [HttpPost]
        public async Task<ActionResult<Room>> CreateRoom(Room room)
        {
            try
            {
                // Add the new room to the database
                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRoom), new { id = room.RoomId }, room);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, "An error occurred while creating the room.");
            }
        }

        // PUT: api/rooms/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, Room room)
        {
            try
            {
                if (id != room.RoomId)
                {
                    return BadRequest();
                }

                // Update the room in the database
                _context.Entry(room).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Reload the updated room from the database
                await _context.Entry(room).ReloadAsync();

                return Ok(room); // Return the updated room in the response
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, "An error occurred while updating the room.");
            }
        }

        // DELETE: api/rooms/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                // Find the room with the specified id
                var room = await _context.Rooms.FindAsync(id);
                if (room == null)
                {
                    return NotFound();
                }

                // Delete the room from the database
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();

                return Ok(room); // Return the deleted room in the response
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate error response
                return StatusCode(500, "An error occurred while deleting the room.");
            }
        }
    }
}
