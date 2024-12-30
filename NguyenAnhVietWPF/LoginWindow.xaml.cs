using BusinessObjects;
using Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Windows;
using System.IO;
using BusinessObjects.Models;
namespace NguyenAnhVietWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ICustomerService iCustomerService;
        private readonly string defaultAdminEmail;
        private readonly string defaultAdminPassword;

        public LoginWindow()
        {
            InitializeComponent();
            iCustomerService = new CustomerService();

            var configPath = "appsettings.json";

            // Load default admin credentials from appsettings.json
            var config = new ConfigurationBuilder()
                .AddJsonFile(configPath, optional: false, reloadOnChange: true)
                .Build();

            // Get the section named "AppSettings"
            var appSettingsSection = config.GetSection("AppSettings");

            // Retrieve the values using indexer
            defaultAdminEmail = appSettingsSection["DefaultAdminEmail"];
            defaultAdminPassword = appSettingsSection["DefaultAdminPassword"];
        }


        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string enteredUsername = txtUser.Text.Trim();
            string enteredPassword = txtPass.Password.Trim();

            // Check if the entered credentials match the default admin credentials
            if (enteredUsername.Equals(defaultAdminEmail, StringComparison.OrdinalIgnoreCase) &&
                enteredPassword.Equals(defaultAdminPassword))
            {
                // Open AdminWindow if admin credentials are matched
                this.Hide();
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
            }
            else
            {
                // Check customer credentials using service
                Customer account = await iCustomerService.GetCustomer(txtUser.Text);
                if (account != null && account.Password.Equals(enteredPassword))
                {
                    // Open CustomerWindow if customer credentials are valid
                    this.Hide();
                    CustomerWindow customerWindow = new CustomerWindow(account);
                    customerWindow.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
