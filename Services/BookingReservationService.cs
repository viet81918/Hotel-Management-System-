using System.Collections.Generic;
using Repositories; // Assuming this namespace contains IBookingReservationRepository
using BusinessObjects.Models; // Assuming this namespace contains BookingReservation

namespace Services
{
    public class BookingReservationService : IBookingReservationService
    {
        private readonly IBookingReservationRepository bookingReservationRepository;

        public BookingReservationService()
        {
            // Initialize repository; consider using dependency injection here
            bookingReservationRepository = new BookingReservationRepository();
        }

        public async Task AddBookingReservation(BookingReservation bookingReservation)
        {
            await bookingReservationRepository.AddBookingReservation(bookingReservation);
        }

        public async Task UpdateBookingReservation(BookingReservation bookingReservation)
        {
            await bookingReservationRepository.UpdateBookingReservation(bookingReservation);
        }

        public async Task DeleteBookingReservation(int bookingReservationID)
        {
            await bookingReservationRepository.DeleteBookingReservation(bookingReservationID);
        }

        public async Task<IEnumerable<BookingReservation>> GetBookingReservation(int? customerId = null, int? reservationId = null)
        {
            return await bookingReservationRepository.GetBookingReservation(customerId, reservationId);
        }

        public async Task<IEnumerable<BookingReservation>> GetBookingReservations()
        {
            return await bookingReservationRepository.GetBookingReservations();
        }
    }
}
