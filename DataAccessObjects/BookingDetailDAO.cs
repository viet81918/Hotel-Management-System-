using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;

namespace DataAccessObjects
{
    public class BookingDetailDAO
    {
        private static  string connectionString = "Data Source=DESKTOP-47R8QHN;Database=FUMiniHotelSystem;User Id=sa;Password=123;TrustServerCertificate=true;Trusted_Connection=SSPI;Encrypt=false";

        // Create
        public static void AddBookingDetail(BookingDetail bookingDetail)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO BookingDetail (BookingReservationID, RoomID, StartDay, EndDate, ActualPrice) " +
                             "VALUES (@BookingReservationID, @RoomID, @StartDay, @EndDate, @ActualPrice)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@BookingReservationID", bookingDetail.BookingReservationID);
                    command.Parameters.AddWithValue("@RoomID", bookingDetail.RoomID);
                    command.Parameters.AddWithValue("@StartDay", bookingDetail.StartDay);
                    command.Parameters.AddWithValue("@EndDate", bookingDetail.EndDate);
                    command.Parameters.AddWithValue("@ActualPrice", bookingDetail.ActualPrice);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while adding a booking detail: " + ex.Message);
                    }
                }
            }
        }

        // Read
        public static List<BookingDetail> GetBookingDetails()
        {
            List<BookingDetail> bookingDetails = new List<BookingDetail>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT BookingReservationID, RoomID, StartDay, EndDate, ActualPrice FROM BookingDetail";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BookingDetail bookingDetail = new BookingDetail
                                (
                                    reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                                    reader.GetInt32(reader.GetOrdinal("RoomID")),
                                    reader.GetDateTime(reader.GetOrdinal("StartDay")),
                                    reader.GetDateTime(reader.GetOrdinal("EndDate")),
                                    reader.GetDecimal(reader.GetOrdinal("ActualPrice"))
                                );
                                bookingDetails.Add(bookingDetail);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while retrieving booking details: " + ex.Message);
                    }
                }
            }

            return bookingDetails;
        }
        public static List<BookingDetail> GetBookingDetail(int bookingReservationID, int? roomID = null)
        {
            List<BookingDetail> bookingDetails = new List<BookingDetail>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT BookingReservationID, RoomID, StartDay, EndDate, ActualPrice FROM BookingDetail " +
                             "WHERE BookingReservationID = @BookingReservationID";

                if (roomID.HasValue)
                {
                    sql += " AND RoomID = @RoomID";
                }

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@BookingReservationID", bookingReservationID);

                    if (roomID.HasValue)
                    {
                        command.Parameters.AddWithValue("@RoomID", roomID.Value);
                    }

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BookingDetail bookingDetail = new BookingDetail
                                (
                                    reader.GetInt32(reader.GetOrdinal("BookingReservationID")),
                                    reader.GetInt32(reader.GetOrdinal("RoomID")),
                                    reader.GetDateTime(reader.GetOrdinal("StartDay")),
                                    reader.GetDateTime(reader.GetOrdinal("EndDate")),
                                    reader.GetDecimal(reader.GetOrdinal("ActualPrice"))
                                );
                                bookingDetails.Add(bookingDetail);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while retrieving the booking details: " + ex.Message);
                    }
                }
            }

            return bookingDetails;
        }

        // Update
        public static void UpdateBookingDetail(BookingDetail bookingDetail)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE BookingDetail SET StartDay = @StartDay, EndDate = @EndDate, ActualPrice = @ActualPrice " +
                             "WHERE BookingReservationID = @BookingReservationID AND RoomID = @RoomID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                 command.Parameters.AddWithValue("@BookingReservationID", bookingDetail.BookingReservationID);
                    command.Parameters.AddWithValue("@RoomID", bookingDetail.RoomID);
                    command.Parameters.AddWithValue("@StartDay", bookingDetail.StartDay);
                    command.Parameters.AddWithValue("@EndDate", bookingDetail.EndDate);
                    command.Parameters.AddWithValue("@ActualPrice", bookingDetail.ActualPrice);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while updating the booking detail: " + ex.Message);
                    }
                }
            }
        }

        // Delete
        public static void DeleteBookingDetail(int bookingReservationID, int roomID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM BookingDetail WHERE BookingReservationID = @BookingReservationID AND RoomID = @RoomID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@BookingReservationID", bookingReservationID);
                    command.Parameters.AddWithValue("@RoomID", roomID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while deleting the booking detail: " + ex.Message);
                    }
                }
            }
        }
    }
}