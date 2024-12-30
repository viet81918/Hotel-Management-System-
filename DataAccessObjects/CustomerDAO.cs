using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class CustomerDAO : SingletonBase<CustomerDAO>
    {
        private  FuminiHotelManagementContext _context ; //loi singleton design partern factory
        public async Task<IEnumerable<Customer>> getCustomers()
        {
            _context = new();
            return await _context.Customers.ToListAsync();
        }
        public async Task AddCustomer(Customer customer)
        {
            _context = new();
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

        }

        public async Task<Customer> GetCustomer(string email)
        {
            _context = new();
            var customerdetail = await _context.Customers.FirstOrDefaultAsync(c => c.EmailAddress == email);
            if (customerdetail == null) return null;
            return customerdetail;
        }
        public async Task<Customer> GetCustomerByID(int id)
        {
            _context = new();
            var customerdetail = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id );
            if (customerdetail == null) return null;
            return customerdetail;
        }
        // Update
        public async Task UpdateCustomer(Customer customer)
        {
            _context = new();
            try
            {
                // Retrieve the existing customer
                var existingItem = await GetCustomerByID(customer.CustomerId);

                if (existingItem != null)
                {
                    // Update the existing customer details
                    _context.Entry(existingItem).CurrentValues.SetValues(customer);
                }
                else
                {
                    // Add the new customer if it doesn't exist
                    _context.Customers.Add(customer);
                }

                // Save changes to the database
                int changes = await _context.SaveChangesAsync();
                if (changes > 0)
                {
                    Console.WriteLine("Customer changes saved successfully to the database.");
                }
                else
                {
                    Console.WriteLine("No customer changes detected to save.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the customer: {ex.Message}");
                throw; // Re-throw the exception to ensure any calling code is aware of the error
            }
        }


        // Delete
        public async Task DeleteCustomer(int customerID)
        {
            _context = new();
            var customerdetail = await GetCustomerByID(customerID);
            if (customerdetail != null)
            {
                _context.Customers.Remove(customerdetail);
                await _context.SaveChangesAsync();
            }
        }
        }
    }

