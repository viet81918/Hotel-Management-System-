using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingReservationRepository
    {
        Task AddBookingReservation(BookingReservation bookingReservation);
        Task<IEnumerable<BookingReservation>> GetBookingReservations();
        Task<IEnumerable<BookingReservation>> GetBookingReservation(int? customerId = null, int? reservationId = null);
        Task UpdateBookingReservation(BookingReservation bookingReservation);
        Task DeleteBookingReservation(int bookingReservationID);
    }
}
