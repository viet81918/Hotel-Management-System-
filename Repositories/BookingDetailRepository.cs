using BusinessObjects.Models;
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
        public async Task AddBookingDetail(BookingDetail bookingDetail)
        {
            await BookingDetailDAO.Instance.AddBookingDetail(bookingDetail);
        }

        public async Task DeleteBookingDetail(int bookingReservationID, int roomID)
        {
            await BookingDetailDAO.Instance.DeleteBookingDetail(bookingReservationID, roomID);
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetail(int? bookingReservationID = null, int? roomID = null)
        {
            return await BookingDetailDAO.Instance.GetBookingDetail(bookingReservationID, roomID);
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetails()
        {
            return await BookingDetailDAO.Instance.GetBookingDetails();
        }

        public async Task UpdateBookingDetail(BookingDetail bookingDetail)
        {
            await BookingDetailDAO.Instance.UpdateBookingDetail(bookingDetail);
        }
    }
}
