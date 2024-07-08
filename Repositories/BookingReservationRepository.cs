using System.Collections.Generic;
using BusinessObjects;
using DataAccessObjects;

namespace Repositories
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        public void AddBookingReservation(BookingReservation bookingReservation) => BookingReservationDAO.AddBookingReservation(bookingReservation);

        public List<BookingReservation> GetBookingReservations() => BookingReservationDAO.GetBookingReservations();

        public List<BookingReservation> GetBookingReservation(int bookingReservationID) => BookingReservationDAO.GetBookingReservation(bookingReservationID);

        public void UpdateBookingReservation(BookingReservation bookingReservation) => BookingReservationDAO.UpdateBookingReservation(bookingReservation);

        public void DeleteBookingReservation(int bookingReservationID) => BookingReservationDAO.DeleteBookingReservation(bookingReservationID);
    }
}
