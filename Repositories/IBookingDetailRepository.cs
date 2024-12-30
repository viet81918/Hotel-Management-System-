using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingDetailRepository
    {
        Task AddBookingDetail(BookingDetail bookingDetail);

        // Read
        Task<IEnumerable<BookingDetail>> GetBookingDetails();

        Task<IEnumerable<BookingDetail>> GetBookingDetail(int? bookingReservationID = null, int? roomID = null);

        // Update
        Task UpdateBookingDetail(BookingDetail bookingDetail);

        // Delete
        Task DeleteBookingDetail(int bookingReservationID, int roomID);
    }
}
