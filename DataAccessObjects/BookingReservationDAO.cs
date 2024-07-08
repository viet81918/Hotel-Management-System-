using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using BusinessObjects;

namespace DataAccessObjects
{
    public class BookingReservationDAO
    {
        private static string connectionString = "Data Source=DESKTOP-47R8QHN;Database=FUMiniHotelSystem;User Id=sa;Password=123;TrustServerCertificate=true;Trusted_Connection=SSPI;Encrypt=false";

        // Create
        public static void AddBookingReservation(BookingReservation bookingReservation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO BookingReservation (BookingReservationID, BookingDate, TotalPrice, CustomerID, BookingStatus) " +
                             "VALUES (@BookingReservationID, @BookingDate, @TotalPrice, @CustomerID, @BookingStatus)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@BookingReservationID", bookingReservation.BookingReservationID);
                    command.Parameters.AddWithValue("@BookingDate", bookingReservation.BookingDate);
                    command.Parameters.AddWithValue("@TotalPrice", bookingReservation.TotalPrice);
                    command.Parameters.AddWithValue("@CustomerID", bookingReservation.CustomerID);
                    command.Parameters.AddWithValue("@BookingStatus", bookingReservation.BookingStatus);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while adding a booking reservation: " + ex.Message);
                    }
                }
            }
        }

        // Read
        public static List<BookingReservation> GetBookingReservations()
        {
            List<BookingReservation> bookingReservations = new List<BookingReservation>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT BookingReservationID, BookingDate, TotalPrice, CustomerID, BookingStatus FROM BookingReservation";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BookingReservation bookingReservation = new BookingReservation
                                (
                                    reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                                    reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                                    reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                                    reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                    reader.GetInt32(reader.GetOrdinal("BookingStatus"))
                                );
                                bookingReservations.Add(bookingReservation);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while retrieving booking reservations: " + ex.Message);
                    }
                }
            }

            return bookingReservations;
        }
        // Read specific booking reservation
        public static List<BookingReservation> GetBookingReservation(int CustomerID)
        {
            List<BookingReservation> bookingReservations = new List<BookingReservation>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT BookingReservationID, BookingDate, TotalPrice, CustomerID, BookingStatus FROM BookingReservation " +
                             "WHERE CustomerID = @CustomerID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BookingReservation bookingReservation = new BookingReservation
                                (
                                    reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                                    reader.GetDateTime(reader.GetOrdinal("BookingDate")),
                                    reader.GetDecimal(reader.GetOrdinal("TotalPrice")),
                                    reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                    reader.GetInt32(reader.GetOrdinal("BookingStatus"))
                                );
                                bookingReservations.Add(bookingReservation);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while retrieving the booking reservations: " + ex.Message);
                    }
                }
            }

            return bookingReservations;
        }

        // Update
        public static void UpdateBookingReservation(BookingReservation bookingReservation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE BookingReservation SET BookingDate = @BookingDate, TotalPrice = @TotalPrice, CustomerID = @CustomerID, BookingStatus = @BookingStatus " +
                             "WHERE BookingReservationID = @BookingReservationID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@BookingReservationID", bookingReservation.BookingReservationID);
                    command.Parameters.AddWithValue("@BookingDate", bookingReservation.BookingDate);
                    command.Parameters.AddWithValue("@TotalPrice", bookingReservation.TotalPrice);
                    command.Parameters.AddWithValue("@CustomerID", bookingReservation.CustomerID);
                    command.Parameters.AddWithValue("@BookingStatus", bookingReservation.BookingStatus);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while updating the booking reservation: " + ex.Message);
                    }
                }
            }
        }

        // Delete
        public static void DeleteBookingReservation(int bookingReservationID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM BookingReservation WHERE BookingReservationID = @BookingReservationID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@BookingReservationID", bookingReservationID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while deleting the booking reservation: " + ex.Message);
                    }
                }
            }
        }
    }
}
