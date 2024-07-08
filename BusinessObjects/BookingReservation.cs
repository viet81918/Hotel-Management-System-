using System;

namespace BusinessObjects
{
    public class BookingReservation
    {
        public int BookingReservationID { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int CustomerID { get; set; }
        public int BookingStatus { get; set; }

        // Parameterized constructor
        public BookingReservation(int bookingReservationID, DateTime bookingDate, decimal totalPrice, int customerID, int bookingStatus)
        {
            BookingReservationID = bookingReservationID;
            BookingDate = bookingDate;
            TotalPrice = totalPrice;
            CustomerID = customerID;
            BookingStatus = bookingStatus;
        }
    }
}
