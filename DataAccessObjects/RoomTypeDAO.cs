using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BusinessObjects;
using Xceed.Wpf.Toolkit;
using System.Windows;

namespace DataAccessObjects
{
 

    public class RoomTypeDAO
    {
        public static string connectionString = "Data Source=DESKTOP-47R8QHN;Database=FUMiniHotelSystem;User Id=sa;Password=123;TrustServerCertificate=true;Trusted_Connection=SSPI;Encrypt=false";

        public static List<RoomType> GetAllRoomTypes()
        {
            List<RoomType> roomTypes = new List<RoomType>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT RoomTypeId, RoomTypeName, TypeDescription, TypeNote FROM RoomType";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        RoomType roomType = new RoomType(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3)
                        );
                        roomTypes.Add(roomType);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roomTypes;
        }


        // Get RoomType by ID
        public static RoomType GetRoomType(int roomTypeID)
        {
            RoomType roomType = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "SELECT RoomTypeID, RoomTypeName, TypeDescription, TypeNote FROM RoomType WHERE RoomTypeID = @RoomTypeID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                roomType = new RoomType
                                (
                                    reader.GetInt32(reader.GetOrdinal("RoomTypeID")),
                                    reader.GetString(reader.GetOrdinal("RoomTypeName")),
                                    reader.GetString(reader.GetOrdinal("TypeDescription")),
                                    reader.GetString(reader.GetOrdinal("TypeNote"))
                                );
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while retrieving the room type: " + ex.Message);
                    }
                }
            }

            return roomType;
        }

        // Add new RoomType
        public static void AddRoomType(RoomType roomType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO RoomType (RoomTypeID,RoomTypeName, TypeDescription, TypeNote) " +
                             "VALUES (@RoomTypeID,@RoomTypeName, @TypeDescription, @TypeNote)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@RoomTypeName", roomType.RoomTypeName);
                    command.Parameters.AddWithValue("@TypeDescription", roomType.TypeDescription);
                    command.Parameters.AddWithValue("@TypeNote", roomType.TypeNote);
                    command.Parameters.AddWithValue("@RoomTypeID", roomType.RoomTypeID);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while adding a new room type: " + ex.Message);
                    }
                }
            }
        }

        // Update existing RoomType
        public static void UpdateRoomType(RoomType roomType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "UPDATE RoomType SET RoomTypeName = @RoomTypeName, TypeDescription = @TypeDescription, TypeNote = @TypeNote " +
                             "WHERE RoomTypeID = @RoomTypeID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@RoomTypeName", roomType.RoomTypeName);
                    command.Parameters.AddWithValue("@TypeDescription", roomType.TypeDescription);
                    command.Parameters.AddWithValue("@TypeNote", roomType.TypeNote);
                    command.Parameters.AddWithValue("@RoomTypeID", roomType.RoomTypeID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while updating the room type: " + ex.Message);
                    }
                }
            }
        }

        // Delete RoomType by ID
        public static void DeleteRoomType(int roomTypeID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM RoomType WHERE RoomTypeID = @RoomTypeID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@RoomTypeID", roomTypeID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while deleting the room type: " + ex.Message);
                    }
                }
            }
        }
    }
}

