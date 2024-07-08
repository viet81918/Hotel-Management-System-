using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingDetailRepository
    {
        void AddBookingDetail(BookingDetail bookingDetail);

        // Read
        List<BookingDetail> GetBookingDetails();

        List<BookingDetail> GetBookingDetail(int bookingReservationID, int roomID);

        // Update
        void UpdateBookingDetail(BookingDetail bookingDetail);

        // Delete
        void DeleteBookingDetail(int bookingReservationID, int roomID);
    }
}
