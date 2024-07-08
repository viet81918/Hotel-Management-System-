using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookingDetailService
    {
        List<BookingDetail> GetBookingDetails();
        List<BookingDetail> GetBookingDetail(int bookingReservationID, int roomID);
        void AddBookingDetail(BookingDetail bookingDetail);
        void UpdateBookingDetail(BookingDetail bookingDetail);
        void DeleteBookingDetail(int bookingReservationID, int roomID);
    }
}