using System.Collections.Generic;
using Repositories; // Assuming this namespace contains IBookingReservationRepository
using BusinessObjects; // Assuming this namespace contains BookingReservation

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

        public void AddBookingReservation(BookingReservation bookingReservation)
        {
            bookingReservationRepository.AddBookingReservation(bookingReservation);
        }

        public void UpdateBookingReservation(BookingReservation bookingReservation)
        {
            bookingReservationRepository.UpdateBookingReservation(bookingReservation);
        }

        public void DeleteBookingReservation(int bookingReservationID)
        {
            bookingReservationRepository.DeleteBookingReservation(bookingReservationID);
        }

        public List<BookingReservation> GetBookingReservation(int bookingReservationID)
        {
            return bookingReservationRepository.GetBookingReservation(bookingReservationID);
        }

        public List<BookingReservation> GetBookingReservations()
        {
            return bookingReservationRepository.GetBookingReservations();
        }
    }
}
