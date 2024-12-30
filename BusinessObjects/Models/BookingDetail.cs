using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class BookingDetail
{

    public BookingDetail() { }

    public BookingDetail(int bookingReservationID, int roomId, DateOnly StartDate, DateOnly EndDate, decimal actualPrice)
    {
        this.BookingReservationId = bookingReservationID;
        this.RoomId = roomId;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
        this.ActualPrice = actualPrice;
    }
    public int BookingReservationId { get; set; }

    public int RoomId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal ActualPrice { get; set; }

    public virtual BookingReservation BookingReservation { get; set; } = null!;

    public virtual RoomInformation Room { get; set; } = null!;
}
