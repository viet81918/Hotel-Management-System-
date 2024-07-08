using System.Collections.Generic;
using BusinessObjects;
using DataAccessObjects;
using Repositories;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService()
        {
            // Initialize repository; consider using dependency injection here
            customerRepository = new CustomerRepository(); // Replace with actual repository implementation
        }

        public void AddCustomer(Customer customer)
        {
            customerRepository.AddCustomer(customer);
        }

        public List<Customer> GetCustomers()
        {
            return customerRepository.GetCustomers();
        }

        public Customer GetCustomer(String email)
        {
            return customerRepository.GetCustomer(email);
        }

        public void UpdateCustomer(Customer customer)
        {
            customerRepository.UpdateCustomer(customer);
        }

        public void DeleteCustomer(int customerID)
        {
            customerRepository.DeleteCustomer(customerID);
        }
    }
}
