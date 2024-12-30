using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Xceed.Wpf.Toolkit;
using System.Windows; // For MessageBoxResult and MessageBox
using System.Drawing;
namespace DataAccessObjects
{
    public class BookingReservationDAO : SingletonBase<BookingReservationDAO>
    {         // Create
        private  FuminiHotelManagementContext _context ; //loi singleton design partern factory
        public async Task AddBookingReservation(BookingReservation bookingReservation)
        {
            _context = new();
            try
            {
                // Check if the entity is already being tracked
                var trackedEntity = _context.ChangeTracker.Entries<BookingReservation>()
                    .FirstOrDefault(e => e.Entity.BookingReservationId == bookingReservation.BookingReservationId);

                if (trackedEntity != null)
                {
                    // Detach the tracked entity
                    _context.Entry(trackedEntity.Entity).State = EntityState.Detached;
                }

                _context.BookingReservations.Add(bookingReservation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding booking: {ex.Message}");
                throw; // Rethrow the exception to ensure any calling code is aware of the error
            }
        }

        // Create


        // Read
        public async Task<IEnumerable<BookingReservation>> GetBookingReservations()
        {
            _context = new();
            return await _context.BookingReservations.ToListAsync();
        }


        // Read specific booking reservation
        public async Task<IEnumerable<BookingReservation>> GetBookingReservation(int? customerId = null, int? reservationId = null)
        {
            using var _context = new FuminiHotelManagementContext(); // Ensure that your DbContext is properly instantiated

            var query = _context.BookingReservations.AsQueryable();

            if (customerId.HasValue)
            {
                query = query.Where(c => c.CustomerId == customerId.Value);
            }

            if (reservationId.HasValue)
            {
                query = query.Where(c => c.BookingReservationId == reservationId.Value);
            }

            var bookingReservations = await query.ToArrayAsync();

            return bookingReservations;
        }


        // Update
        public async Task UpdateBookingReservation(BookingReservation bookingReservation)
        {
            using var _context = new FuminiHotelManagementContext(); // Ensure your DbContext is properly instantiated

            var existingReservation = await _context.BookingReservations
                .FirstOrDefaultAsync(br => br.BookingReservationId == bookingReservation.BookingReservationId);

            if (existingReservation != null)
            {
                // Update the properties of the existing reservation
                existingReservation.TotalPrice = bookingReservation.TotalPrice;
                existingReservation.BookingDate = bookingReservation.BookingDate;
                existingReservation.BookingStatus = bookingReservation.BookingStatus;
                existingReservation.CustomerId = bookingReservation.CustomerId;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Booking reservation not found.");
            }
        }

        // Delete
        public async Task DeleteBookingReservation(int bookingReservationID)
        {
            _context = new();

        }
        }
    }

