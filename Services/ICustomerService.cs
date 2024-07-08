using BusinessObjects;

namespace   Services
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);
        List<Customer> GetCustomers();
        Customer GetCustomer(String email);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerID);
    }
}
