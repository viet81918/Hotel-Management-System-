using System.Collections.Generic;
using BusinessObjects.Models;
using DataAccessObjects;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public async Task AddCustomer(Customer customer)
        {
            await CustomerDAO.Instance.AddCustomer(customer);
        }

        public async Task DeleteCustomer(int customerID)
        {
            await CustomerDAO.Instance.DeleteCustomer(customerID);
        }

        public async Task<Customer> GetCustomer(string email)
        {
            return await CustomerDAO.Instance.GetCustomer(email);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
           return await CustomerDAO.Instance.getCustomers();
        }

        public async Task UpdateCustomer(Customer customer)
        {
           await CustomerDAO.Instance.UpdateCustomer(customer);
        }
    }
}
