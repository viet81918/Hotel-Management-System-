using System.Collections.Generic;
using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public async Task AddBookingReservation(BookingReservation bookingReservation)
        {
            await BookingReservationDAO.Instance.AddBookingReservation(bookingReservation);
        }

        public async Task DeleteBookingReservation(int bookingReservationID)
        {
            await BookingReservationDAO.Instance.DeleteBookingReservation(bookingReservationID);
        }

        public async Task<IEnumerable<BookingReservation>> GetBookingReservation(int? customerId = null, int? reservationId = null)
        {
            return await BookingReservationDAO.Instance.GetBookingReservation(customerId,reservationId);
        }

        public async Task<IEnumerable<BookingReservation>> GetBookingReservations()
        {
            return await BookingReservationDAO.Instance.GetBookingReservations();
        }

        public async Task UpdateBookingReservation(BookingReservation bookingReservation)
        {
            await BookingReservationDAO.Instance.UpdateBookingReservation(bookingReservation);
        }
    }
}