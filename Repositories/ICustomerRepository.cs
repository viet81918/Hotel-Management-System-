using BusinessObjects;

namespace Repositories
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        List<Customer> GetCustomers();
        Customer GetCustomer(String email);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerID);
    }
}
