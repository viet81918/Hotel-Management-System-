using BusinessObjects;
using Microsoft.Identity.Client.NativeInterop;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NguyenAnhVietWPF
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private Customer customer;
        private readonly ICustomerService iCustomerService;
        public CustomerWindow(Customer customer)
        {
            InitializeComponent();
            iCustomerService = new CustomerService();
            this.customer = customer; // Store the customer data in the private field
            DataContext = this.customer; // Set DataContext to bind controls to customer properties
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Update customer properties from UI controls
                customer.CustomerFullName = txtCustomerFullName.Text;
                customer.Telephone = txtTelephone.Text;
                customer.EmailAddress = txtEmailAddress.Text;
                customer.Password = txtCustomerPassWord.Text;
                // Update other properties as needed
                iCustomerService.UpdateCustomer(customer);
                // Call a service or DAO to update the customer in database
                // Example:
                // CustomerService.UpdateCustomer(customer);

                MessageBox.Show("Profile updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update profile: {ex.Message}");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            BookingWindow bookingWindow = new BookingWindow(customer);
            bookingWindow.Show();
        }

    }
}
    
