using HotelBookingSystem.Models;

namespace HotelBookingSystem.Repository
{
    public interface IFilterAndCountRepository
    {
        public int GetRoomCount(int id);

        public IEnumerable<Hotel> GetHotelsByFilter(string? location, decimal? minPrice, decimal? maxPrice);

        IEnumerable<Hotel> GetHotelsByLocation(string location);


    }
}

   
