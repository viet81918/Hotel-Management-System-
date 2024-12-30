using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class BookingReservation
{

    public BookingReservation()
    {

    }
    public BookingReservation(int bookingReservationID, DateOnly bookingDate, decimal actualPrice, int customerId, string status)
    {
        this.BookingReservationId = bookingReservationID;
        this.BookingDate = bookingDate;
        this.TotalPrice = actualPrice;
        this.CustomerId = customerId;
        this.BookingStatus = status;
    }

    public int BookingReservationId { get; set; }

    public DateOnly BookingDate { get; set; }

    public decimal TotalPrice { get; set; }

    public int CustomerId { get; set; }

    public string BookingStatus { get; set; } = null!;

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Customer Customer { get; set; } = null!;
}
