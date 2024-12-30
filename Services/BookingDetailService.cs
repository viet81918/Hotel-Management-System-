using BusinessObjects.Models;
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

        public async Task AddBookingDetail(BookingDetail bookingDetail)
        {
            await bookingDetailRepository.AddBookingDetail(bookingDetail);
        }

        public async Task DeleteBookingDetail(int bookingReservationID, int roomID)
        {
            await bookingDetailRepository.DeleteBookingDetail(bookingReservationID, roomID);
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetail(int? bookingReservationID = null , int? roomID = null)
        {
            return await bookingDetailRepository.GetBookingDetail(bookingReservationID, roomID);
        }

        public async Task<IEnumerable<BookingDetail>> GetBookingDetails()
        {
            return await bookingDetailRepository.GetBookingDetails();
        }

        public async Task UpdateBookingDetail(BookingDetail bookingDetail)
        {
            await bookingDetailRepository.UpdateBookingDetail(bookingDetail);
        }   
    }
}