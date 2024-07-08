using BusinessObjects;
using DataAccessObjects;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IBookingDetailRepository bookingDetailRepository;

        public BookingDetailService()
        {
            // Instantiate the repository; you might want to use dependency injection instead
            bookingDetailRepository = new BookingDetailRepository();
        }

        public void AddBookingDetail(BookingDetail bookingDetail)
        {
            bookingDetailRepository.AddBookingDetail(bookingDetail);
        }

        public void DeleteBookingDetail(int bookingReservationID, int roomID)
        {
            bookingDetailRepository.DeleteBookingDetail(bookingReservationID, roomID);
        }

        public List<BookingDetail> GetBookingDetail(int bookingReservationID, int roomID)
        {
            return bookingDetailRepository.GetBookingDetail(bookingReservationID, roomID);
        }

        public List<BookingDetail> GetBookingDetails()
        {
            return bookingDetailRepository.GetBookingDetails();
        }

        public void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            bookingDetailRepository.UpdateBookingDetail(bookingDetail);
        }
    }
}