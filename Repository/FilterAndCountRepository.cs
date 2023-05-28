using HotelBookingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingSystem.Repository
{
    public class FilterAndCountRepository : IFilterAndCountRepository
    {
        private readonly HotelDbContext _context;

        public FilterAndCountRepository(HotelDbContext context)
        {
            _context = context;
        }

        public int GetRoomCount(int id)
        {
            try
            {
                return _context.Set<Room>().Count(r => r.HotelId == id && r.IsOccupied == true);
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                Console.WriteLine("Error while getting the count of room: " + ex.Message);
                throw; // Re-throw the exception to be handled at the higher level
            }
        }

        public IEnumerable<Hotel> GetHotelsByFilter(string? location, decimal? minPrice, decimal? maxPrice)
        {
            try
            {
                var hotels = _context.Set<Hotel>().AsQueryable();

                if (!string.IsNullOrEmpty(location))
                    hotels = hotels.Where(h => h.Location.Contains(location));

                if (minPrice != null)
                    hotels = hotels.Where(h => h.MinPrice >= minPrice);

                if (maxPrice != null)
                    hotels = hotels.Where(h => h.MinPrice <= maxPrice);

                return hotels.ToList();
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                Console.WriteLine("Error retrieving hotels by filter: " + ex.Message);
                throw; // Re-throw the exception to be handled at the higher level
            }
        }

    }
}
