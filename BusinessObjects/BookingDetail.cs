using System;

namespace BusinessObjects
{
    public class BookingDetail
    {
        public int BookingReservationID { get; set; }
        public int RoomID { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ActualPrice { get; set; }

        // Parameterized constructor
        public BookingDetail(int bookingReservationID, int roomID, DateTime startDay, DateTime endDate, decimal actualPrice)
        {
            BookingReservationID = bookingReservationID;
            RoomID = roomID;
            StartDay = startDay;
            EndDate = endDate;
            ActualPrice = actualPrice;
        }
    }
}
