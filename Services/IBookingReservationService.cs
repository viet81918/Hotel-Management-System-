using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using BusinessObjects; // Assuming this namespace contains your business objects

namespace Services
{
    public interface IBookingReservationService
    {
        Task AddBookingReservation(BookingReservation bookingReservation);
        Task UpdateBookingReservation(BookingReservation bookingReservation);
        Task DeleteBookingReservation(int bookingReservationID);
        Task<IEnumerable<BookingReservation>> GetBookingReservation(int? customerId = null, int? reservationId = null);
        Task<IEnumerable<BookingReservation>> GetBookingReservations();
    }
}
