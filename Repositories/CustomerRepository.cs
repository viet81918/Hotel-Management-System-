using System.Collections.Generic;
using BusinessObjects;
using DataAccessObjects;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer) => CustomerDAO.AddCustomer(customer);

        public List<Customer> GetCustomers() => CustomerDAO.GetCustomers();

        public Customer GetCustomer(String email) => CustomerDAO.GetCustomer(email);

        public void UpdateCustomer(Customer customer) => CustomerDAO.UpdateCustomer(customer);

        public void DeleteCustomer(int customerID) => CustomerDAO.DeleteCustomer(customerID);
    }
}
