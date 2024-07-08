using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BusinessObjects;

namespace DataAccessObjects
{
    public class CustomerDAO
    {
        public static string connectionString = "Data Source=DESKTOP-47R8QHN;Database=FUMiniHotelSystem;User Id=sa;Password=123;TrustServerCertificate=true;Trusted_Connection=SSPI;Encrypt=false";

        public static  List<Customer> getCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT CustomerID, CustomerFullName, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password FROM Customer";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer = new Customer
                                (
                                    reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                    reader.GetString(reader.GetOrdinal("CustomerFullName")),
                                    reader.GetString(reader.GetOrdinal("Telephone")),
                                    reader.GetString(reader.GetOrdinal("EmailAddress")),
                                   reader.GetDateTime(reader.GetOrdinal("CustomerBirthday")),
                                     reader.GetInt32(reader.GetOrdinal("CustomerStatus")),
                                    reader.GetString(reader.GetOrdinal("Password"))
                                );
                                customers.Add(customer);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while retrieving customers: " + ex.Message);
                    }
                }
            }

            return customers;
        }
        public  static void AddCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Customer (CustomerID,CustomerFullName, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password) " +
                             "VALUES (@CustomerID, @CustomerFullName, @Telephone, @EmailAddress, @CustomerBirthday, @CustomerStatus, @Password)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    command.Parameters.AddWithValue("@CustomerFullName", customer.CustomerFullName);
                    command.Parameters.AddWithValue("@Telephone", customer.Telephone);
                    command.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                    command.Parameters.AddWithValue("@CustomerBirthday", customer.CustomerBirthday);
                    command.Parameters.AddWithValue("@CustomerStatus", customer.CustomerStatus);
                    command.Parameters.AddWithValue("@Password", customer.Password);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while adding a customer: " + ex.Message);
                    }
                }
            }
        }

        // Read
        public static  List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT CustomerID, CustomerFullName, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password FROM Customer";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Customer customer = new Customer
                                (
                                    reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                    reader.GetString(reader.GetOrdinal("CustomerFullName")),
                                    reader.GetString(reader.GetOrdinal("Telephone")),
                                    reader.GetString(reader.GetOrdinal("EmailAddress")),
                                    reader.GetDateTime(reader.GetOrdinal("CustomerBirthday")),
                                    reader.GetInt32(reader.GetOrdinal("CustomerStatus")),
                                    reader.GetString(reader.GetOrdinal("Password"))
                                );
                                customers.Add(customer);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while retrieving customers: " + ex.Message);
                    }
                }
            }

            return customers;
        }
        public static Customer GetCustomer(string email)
        {
            Customer customer = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT CustomerID, CustomerFullName, Telephone, EmailAddress, CustomerBirthday, CustomerStatus, Password FROM Customer " +
                             "WHERE EmailAddress = @EmailAddress";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@EmailAddress", email);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customer = new Customer
                                (
                                    reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                    reader.GetString(reader.GetOrdinal("CustomerFullName")),
                                    reader.GetString(reader.GetOrdinal("Telephone")),
                                    reader.GetString(reader.GetOrdinal("EmailAddress")),
                                    reader.GetDateTime(reader.GetOrdinal("CustomerBirthday")),
                                    reader.GetInt32(reader.GetOrdinal("CustomerStatus")),
                                    reader.GetString(reader.GetOrdinal("Password"))
                                );
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while retrieving the customer by email: " + ex.Message);
                    }
                }
            }

            return customer;
        }
        // Update
        public static void UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Customer SET CustomerFullName = @CustomerFullName, Telephone = @Telephone, EmailAddress = @EmailAddress, " +
                             "CustomerBirthday = @CustomerBirthday, CustomerStatus = @CustomerStatus, Password = @Password " +
                             "WHERE CustomerID = @CustomerID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    command.Parameters.AddWithValue("@CustomerFullName", customer.CustomerFullName);
                    command.Parameters.AddWithValue("@Telephone", customer.Telephone);
                    command.Parameters.AddWithValue("@EmailAddress", customer.EmailAddress);
                    command.Parameters.AddWithValue("@CustomerBirthday", customer.CustomerBirthday);
                    command.Parameters.AddWithValue("@CustomerStatus", customer.CustomerStatus);
                    command.Parameters.AddWithValue("@Password", customer.Password);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while updating the customer: " + ex.Message);
                    }
                }
            }
        }

        // Delete
        public static void DeleteCustomer(int customerID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Customer WHERE CustomerID = @CustomerID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while deleting the customer: " + ex.Message);
                    }
                }
            }
        }
    }
}
