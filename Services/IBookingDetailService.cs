using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookingDetailService
    {
       Task<IEnumerable<BookingDetail>> GetBookingDetails();
        Task<IEnumerable<BookingDetail>> GetBookingDetail(int? bookingReservationID = null, int? roomID = null);
        Task AddBookingDetail(BookingDetail bookingDetail);
        Task UpdateBookingDetail(BookingDetail bookingDetail);
        Task DeleteBookingDetail(int bookingReservationID, int roomID);
    }
}