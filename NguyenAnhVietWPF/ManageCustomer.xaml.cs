using BusinessObjects;
using DataAccessObjects;
using Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace NguyenAnhVietWPF
{
    public partial class ManageCustomer : Window
    {
        private readonly ICustomerService iCustomerService;

        public ManageCustomer()
        {
            InitializeComponent();
            iCustomerService = new CustomerService();
            LoadCustomerList();
        }

        private void LoadCustomerList()
        {
            try
            {
                var customerList = CustomerDAO.getCustomers();
                dgData.ItemsSource = customerList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading customer information");
            }
            finally
            {
                ResetInput();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate input controls
                if (string.IsNullOrWhiteSpace(txtCustomerId.Text) ||
                    string.IsNullOrWhiteSpace(txtCusFullName.Text) ||
                    string.IsNullOrWhiteSpace(txtCusTelephone.Text) ||
                    string.IsNullOrWhiteSpace(txtCusEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtCusBirth.Text) ||
                    string.IsNullOrWhiteSpace(txtCusStatus.Text) ||
                    string.IsNullOrWhiteSpace(txtCusPassword.Text)) // PasswordBox validation
                {
                    MessageBox.Show("One or more input fields are empty.");
                    return;
                }

                // Create Customer object
                Customer customer = new Customer
                (
                    int.Parse(txtCustomerId.Text),
                    txtCusFullName.Text,
                    txtCusTelephone.Text,
                    txtCusEmail.Text,
                    DateTime.Parse(txtCusBirth.Text),
                    int.Parse(txtCusStatus.Text), // Assuming CustomerStatus is an int
                    txtCusPassword.Text     // Add other properties as needed
                );

                // Save customer information
                iCustomerService.AddCustomer(customer);

                // Refresh customer list
                LoadCustomerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem == null || !(dgData.SelectedItem is Customer selectedCustomer))
                    return;

                txtCustomerId.Text = selectedCustomer.CustomerID.ToString();
                txtCusFullName.Text = selectedCustomer.CustomerFullName;
                txtCusTelephone.Text = selectedCustomer.Telephone;
                txtCusEmail.Text = selectedCustomer.EmailAddress;
                txtCusBirth.Text = selectedCustomer.CustomerBirthday.ToString("dd/MM/yyyy");
                txtCusStatus.Text = selectedCustomer.CustomerStatus.ToString();
                txtCusPassword.Text = selectedCustomer.Password;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem == null || !(dgData.SelectedItem is Customer selectedCustomer))
                {
                    MessageBox.Show("Please select a customer to update.");
                    return;
                }

                // Update selected customer information
                selectedCustomer.CustomerFullName = txtCusFullName.Text;
                selectedCustomer.Telephone = txtCusTelephone.Text;
                selectedCustomer.EmailAddress = txtCusEmail.Text;
                selectedCustomer.CustomerBirthday = DateTime.ParseExact(txtCusBirth.Text, "dd/MM/yyyy", null); // Example format, adjust as per your requirement
                selectedCustomer.CustomerStatus = int.Parse(txtCusStatus.Text);
                selectedCustomer.Password = txtCusPassword.Text;
                iCustomerService.UpdateCustomer(selectedCustomer); // Update customer in database

                // Notify DataGrid of the update
                dgData.Items.Refresh();
                ResetInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem == null || !(dgData.SelectedItem is Customer selectedCustomer))
                {
                    MessageBox.Show("Please select a customer to delete.");
                    return;
                }

                // Delete selected customer
                iCustomerService.DeleteCustomer(selectedCustomer.CustomerID); // Assuming DeleteCustomer method in your service

                // Refresh data grid
                LoadCustomerList(); // Example method to reload customer data

                MessageBox.Show("Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void ResetInput()
        {
            txtCustomerId.Text = "";
            txtCusFullName.Text = "";
            txtCusTelephone.Text = "";
            txtCusEmail.Text = "";
            txtCusBirth.Text = "";
            txtCusStatus.Text = "";
            txtCusPassword.Text = "";
        }
      
    }
}
