using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Customer
{

    public Customer()
    {
    }


    public Customer(int v1, string text1, string text2, string text3, DateOnly dateTime, int v2, string text4)
    {
        this.CustomerId = v1;
        this.CustomerFullName = text1;
        this.Telephone = text2;
        this.EmailAddress = text3;
        this.CustomerBirthday = dateTime;
        this.CustomerStatus = v2;
        this.Password = text4;
    }

    public int CustomerId { get; set; }

    public string CustomerFullName { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public DateOnly CustomerBirthday { get; set; }

    public int CustomerStatus { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<BookingReservation> BookingReservations { get; set; } = new List<BookingReservation>();
}
