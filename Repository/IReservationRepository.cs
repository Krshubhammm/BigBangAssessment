using HotelBookingSystem.Models;

namespace HotelBookingSystem.Repository
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task<Reservation> GetReservationAsync(int id);
        Task CreateReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int id);
    }
}
