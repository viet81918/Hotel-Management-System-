using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;

namespace DataAccessObjects
{
    public class BookingDetailDAO : SingletonBase<BookingDetailDAO>
    {
        private  FuminiHotelManagementContext _context ; //loi singleton design partern factory

        // Create
        public async Task AddBookingDetail(BookingDetail bookingDetail)
        {
            _context = new();
            _context.BookingDetails.Add(bookingDetail);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<IEnumerable<BookingDetail>> GetBookingDetails()
        {
            _context = new();
            return await _context.BookingDetails.ToListAsync();
        }
        public async Task<BookingDetail> GetBookingDetailReservationandRoom(int? bookingReservationID = null, int? roomID = null)
        {
            _context = new();

            // Check if both parameters are null
            if (bookingReservationID == null && roomID == null)
            {
                return null; // or handle it accordingly
            }

            // Query based on provided parameters
            IQueryable<BookingDetail> query = _context.BookingDetails;

            if (bookingReservationID != null)
            {
                query = query.Where(c => c.BookingReservationId == bookingReservationID);
            }

            if (roomID != null)
            {
                query = query.Where(c => c.RoomId == roomID);
            }

            // Retrieve the first matching booking detail
            var bookingdetail = await query.FirstOrDefaultAsync();

            return bookingdetail;
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetail(int? bookingReservationID = null, int? roomID = null)
{
    _context = new();

    // Query based on provided parameters
    IQueryable<BookingDetail> query = _context.BookingDetails;

    if (bookingReservationID != null)
    {
        query = query.Where(c => c.BookingReservationId == bookingReservationID);
    }

    if (roomID != null)
    {
        query = query.Where(c => c.RoomId == roomID);
    }

    // Retrieve the list of matching booking details
    var bookingDetails = await query.ToListAsync();

    return bookingDetails;
}

        // Update
        public async Task UpdateBookingDetail(BookingDetail bookingDetail)
        {
            _context = new();
            var existingItem = await GetBookingDetailReservationandRoom(bookingDetail.BookingReservationId, bookingDetail.RoomId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(bookingDetail);
            }
            else
            {
                _context.BookingDetails.Add(bookingDetail);
            }
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteBookingDetail(int bookingReservationID, int roomID)
        {
            _context = new();
            var bookingdetail = await GetBookingDetailReservationandRoom(bookingReservationID, roomID);
            if (bookingdetail != null)
            {
                _context.BookingDetails.Remove(bookingdetail);
                await _context.SaveChangesAsync();
            }
        }
    }
}