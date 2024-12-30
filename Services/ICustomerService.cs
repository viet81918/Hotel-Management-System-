using BusinessObjects.Models;

namespace   Services
{
    public interface ICustomerService
    {
        Task AddCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(String email);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int customerID);
    }
}
