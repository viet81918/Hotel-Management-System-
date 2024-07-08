using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BusinessObjects;

namespace DataAccessObjects
{
    public class RoomInformationDAO
    {
        public static string connectionString = "Data Source=DESKTOP-47R8QHN;Database=FUMiniHotelSystem;User Id=sa;Password=123;TrustServerCertificate=true;Trusted_Connection=SSPI;Encrypt=false";

        public static List<RoomInformation> GetRoomInformations()
        {
            List<RoomInformation> roomInformations = new List<RoomInformation>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT RoomID, RoomNumber, RoomDescription, RoomMaxCapacity, RoomStatus, RoomPricePerDate, RoomTypeID FROM RoomInformation";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RoomInformation roomInformation = new RoomInformation
                                (
                                    reader.GetInt32(reader.GetOrdinal("RoomID")),
                                    reader.GetString(reader.GetOrdinal("RoomNumber")),
                                    reader.GetString(reader.GetOrdinal("RoomDescription")),
                                    reader.GetInt32(reader.GetOrdinal("RoomMaxCapacity")),
                                    reader.GetInt32(reader.GetOrdinal("RoomStatus")),
                                    (double)reader.GetDecimal(reader.GetOrdinal("RoomPricePerDate")), // Use GetDecimal instead of GetDouble
                                    reader.GetInt32(reader.GetOrdinal("RoomTypeID"))
                                );
                                roomInformations.Add(roomInformation);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while retrieving room information: " + ex.Message);
                    }
                }
            }

            return roomInformations;
        }

        public static void AddRoomInformation(RoomInformation roomInformation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO RoomInformation (RoomID,RoomNumber, RoomDescription, RoomMaxCapacity, RoomStatus, RoomPricePerDate, RoomTypeID) " +
                             "VALUES (@RoomID,@RoomNumber, @RoomDescription, @RoomMaxCapacity, @RoomStatus, @RoomPricePerDate, @RoomTypeID)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@RoomID", roomInformation.RoomID);
                    command.Parameters.AddWithValue("@RoomNumber", roomInformation.RoomNumber);
                    command.Parameters.AddWithValue("@RoomDescription", roomInformation.RoomDescription);
                    command.Parameters.AddWithValue("@RoomMaxCapacity", roomInformation.RoomMaxCapacity);
                    command.Parameters.AddWithValue("@RoomStatus", roomInformation.RoomStatus);
                    command.Parameters.AddWithValue("@RoomPricePerDate", roomInformation.RoomPricePerDate);
                    command.Parameters.AddWithValue("@RoomTypeID", roomInformation.RoomTypeID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while adding room information: " + ex.Message);
                    }
                }
            }
        }
        public static RoomInformation GetRoomInformation(int roomID)
        {
            RoomInformation roomInformation = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT RoomID, RoomNumber, RoomDescription, RoomMaxCapacity, RoomStatus, RoomPricePerDate, RoomTypeID " +
                             "FROM RoomInformation " +
                             "WHERE RoomID = @RoomID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@RoomID", roomID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                roomInformation = new RoomInformation
                                (
                                     reader.GetInt32(reader.GetOrdinal("RoomID")),
                                   reader.GetString(reader.GetOrdinal("RoomNumber")),
                                   reader.GetString(reader.GetOrdinal("RoomDescription")),
                                     reader.GetInt32(reader.GetOrdinal("RoomMaxCapacity")),
                                reader.GetInt32(reader.GetOrdinal("RoomStatus")),
                                   reader.GetDouble(reader.GetOrdinal("RoomPricePerDate")),
                                  reader.GetInt32(reader.GetOrdinal("RoomTypeID"))
                                );
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while retrieving room information: " + ex.Message);
                    }
                }
            }

            return roomInformation;
        }
        public static void UpdateRoomInformation(RoomInformation roomInformation)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE RoomInformation SET RoomNumber = @RoomNumber, RoomDescription = @RoomDescription, " +
                             "RoomMaxCapacity = @RoomMaxCapacity, RoomStatus = @RoomStatus, RoomPricePerDate = @RoomPricePerDate, " +
                             "RoomTypeID = @RoomTypeID WHERE RoomID = @RoomID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@RoomID", roomInformation.RoomID);
                    command.Parameters.AddWithValue("@RoomNumber", roomInformation.RoomNumber);
                    command.Parameters.AddWithValue("@RoomDescription", roomInformation.RoomDescription);
                    command.Parameters.AddWithValue("@RoomMaxCapacity", roomInformation.RoomMaxCapacity);
                    command.Parameters.AddWithValue("@RoomStatus", roomInformation.RoomStatus);
                    command.Parameters.AddWithValue("@RoomPricePerDate", roomInformation.RoomPricePerDate);
                    command.Parameters.AddWithValue("@RoomTypeID", roomInformation.RoomTypeID);


                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while updating room information: " + ex.Message);
                    }
                }
            }
        }

        public static  void DeleteRoomInformation(int roomID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM RoomInformation WHERE RoomID = @RoomID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@RoomID", roomID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while deleting room information: " + ex.Message);
                    }
                }
            }
        }
        public List<RoomInformation> SearchRoomInformationsByDescription(string description)
        {
            List<RoomInformation> roomInformations = new List<RoomInformation>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT RoomID, RoomNumber, RoomDescription, RoomMaxCapacity, RoomStatus, RoomPricePerDate, RoomTypeID FROM RoomInformation WHERE RoomDescription LIKE @Description";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Description", "%" + description + "%");

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RoomInformation roomInformation = new RoomInformation
                               (reader.GetInt32(reader.GetOrdinal("RoomID")),
                                  reader.GetString(reader.GetOrdinal("RoomNumber")),
                                   reader.GetString(reader.GetOrdinal("RoomDescription")),
                                  reader.GetInt32(reader.GetOrdinal("RoomMaxCapacity")),
                                 reader.GetInt32(reader.GetOrdinal("RoomStatus")),
                                  reader.GetDouble(reader.GetOrdinal("RoomPricePerDate")),
                               reader.GetInt32(reader.GetOrdinal("RoomTypeID"))
                               );
                                roomInformations.Add(roomInformation);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while searching room information: " + ex.Message);
                    }
                }
            }

            return roomInformations;
        }

    }
}
