using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingReservationRepository
    {
        void AddBookingReservation(BookingReservation bookingReservation);
        List<BookingReservation> GetBookingReservations();
        List<BookingReservation> GetBookingReservation(int bookingReservationID);
        void UpdateBookingReservation(BookingReservation bookingReservation);
        void DeleteBookingReservation(int bookingReservationID);
    }
}
