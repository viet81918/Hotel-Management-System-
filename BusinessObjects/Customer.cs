using System;

namespace BusinessObjects
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerFullName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CustomerBirthday { get; set; }
        public int CustomerStatus { get; set; }
        public string Password { get; set; }

        // Parameterized constructor
        public Customer(int customerID, string customerFullName, string telephone, string emailAddress, DateTime customerBirthday, int customerStatus, string password)
        {
            CustomerID = customerID;
            CustomerFullName = customerFullName;
            Telephone = telephone;
            EmailAddress = emailAddress;
            CustomerBirthday = customerBirthday;
            CustomerStatus = customerStatus;
            Password = password;
        }
    }
}
