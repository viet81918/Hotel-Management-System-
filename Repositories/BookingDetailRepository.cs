using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        public void AddBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.AddBookingDetail(bookingDetail);

        // Read
        public List<BookingDetail> GetBookingDetails() => BookingDetailDAO.GetBookingDetails();

        public List<BookingDetail> GetBookingDetail(int bookingReservationID, int roomID) => BookingDetailDAO.GetBookingDetail(bookingReservationID, roomID);

        // Update
        public void UpdateBookingDetail(BookingDetail bookingDetail) => BookingDetailDAO.UpdateBookingDetail(bookingDetail);

        // Delete
        public void DeleteBookingDetail(int bookingReservationID, int roomID) => BookingDetailDAO.DeleteBookingDetail(bookingReservationID, roomID);
    }
}
