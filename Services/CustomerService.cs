using System.Collections.Generic;
using BusinessObjects.Models;
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

        public async Task AddCustomer(Customer customer)
        {
            await customerRepository.AddCustomer(customer);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await customerRepository.GetCustomers();
        }

        public async Task<Customer> GetCustomer(String email)
        {
            return await customerRepository.GetCustomer(email);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            await customerRepository.UpdateCustomer(customer);
        }

        public async Task DeleteCustomer(int customerID)
        {
            await customerRepository.DeleteCustomer(customerID);
        }
    }
}
