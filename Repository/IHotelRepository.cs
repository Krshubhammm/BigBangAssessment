using HotelBookingSystem.Models;

namespace HotelBookingSystem.Repository
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetHotelsAsync();
        Task<Hotel> GetHotelAsync(int id);
        Task CreateHotelAsync(Hotel hotel);
        Task UpdateHotelAsync(Hotel hotel);
        Task DeleteHotelAsync(int id);
      
    }
}
