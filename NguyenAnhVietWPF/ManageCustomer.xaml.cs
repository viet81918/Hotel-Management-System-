using BusinessObjects;
using BusinessObjects.Models;
using DataAccessObjects;
using Services;
using System;
using System.Globalization;
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
            InitializeAsync();
        }

        private async Task InitializeAsync()
        {

            await LoadCustomerList();
        }
        private async Task LoadCustomerList()
        {
            try
            {
                var customerList = await iCustomerService.GetCustomers();
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
                   ParseDateOnly(txtCusBirth.Text),
                    int.Parse(txtCusStatus.Text), // Assuming CustomerStatus is an int
                    txtCusPassword.Text     // Add other properties as needed
                );

                // Save customer information
                iCustomerService.AddCustomer(customer);

                // Refresh customer list
                InitializeAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        public DateOnly ParseDateOnly(string dateString)
        {
            try
            {
                // Define the format of the date string
                string format = "dd/MM/yyyy";

                // Parse the string to DateTime
                DateTime dateTime = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

                // Convert DateTime to DateOnly
                return DateOnly.FromDateTime(dateTime);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"An error occurred while parsing the date: {ex.Message}");
                throw; // Re-throw the exception to ensure any calling code is aware of the error
            }
        }

            private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgData.SelectedItem == null || !(dgData.SelectedItem is Customer selectedCustomer))
                    return;

                txtCustomerId.Text = selectedCustomer.CustomerId.ToString();
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
        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the search keyword from the text box
                string searchWord = txtSearchWord.Text.Trim();

                // Fetch all room information
                var allCus = await iCustomerService.GetCustomers();

                // Filter rooms by name using LINQ
                var filteredCuss = allCus
                    .Where(r => r.CustomerFullName.Contains(searchWord, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (filteredCuss == null || !filteredCuss.Any())
                {
                    MessageBox.Show("No Cus found.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    dgData.ItemsSource = null;  // Clear the DataGrid if no results
                }
                else
                {
                    // Update the DataGrid with the search results
                    dgData.ItemsSource = filteredCuss;

                    ResetInput();
                }
            }
            catch (Exception ex)
            {
                // Display inner exception details
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : string.Empty;
                MessageBox.Show($"An error occurred: {ex.Message}\nInner Exception: {innerExceptionMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                selectedCustomer.CustomerBirthday = DateOnly.ParseExact(txtCusBirth.Text, "dd/MM/yyyy", null); // Example format, adjust as per your requirement
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
                var result = MessageBox.Show($"Are you sure you want to delete Customer {selectedCustomer.CustomerId}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    return; // User chose not to delete
                }

                // Delete selected customer
                iCustomerService.DeleteCustomer(selectedCustomer.CustomerId); // Assuming DeleteCustomer method in your service

                // Refresh data grid
                InitializeAsync(); // Example method to reload customer data

                MessageBox.Show("Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private async void btnManageRooms_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AdminWindow manageRoom = new AdminWindow();
            manageRoom.ShowDialog();
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
