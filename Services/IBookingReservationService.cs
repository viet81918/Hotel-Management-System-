using BusinessObjects;
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
        void AddBookingReservation(BookingReservation bookingReservation);
        void UpdateBookingReservation(BookingReservation bookingReservation);
        void DeleteBookingReservation(int bookingReservationID);
        List<BookingReservation> GetBookingReservation(int bookingReservationID);
        List<BookingReservation> GetBookingReservations();
    }
}
