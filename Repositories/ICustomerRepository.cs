using BusinessObjects.Models;

namespace Repositories
{
    public interface ICustomerRepository
    {
        Task AddCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(String email);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int customerID);
    }
}
