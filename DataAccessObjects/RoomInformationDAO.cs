using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class RoomInformationDAO : SingletonBase<RoomInformationDAO>
    {

        private FuminiHotelManagementContext _context; //loi singleton design partern factory
        public async Task<IEnumerable<RoomInformation>> GetRoomInformations()
        {
            _context = new();
            return await _context.RoomInformations.ToListAsync();
        }
        public async Task AddRoomInformation(RoomInformation roomInformation)
        {
            _context = new();
            _context.RoomInformations.Add(roomInformation);
            await _context.SaveChangesAsync();
        }
        public async Task<RoomInformation> GetRoomInformation(int roomID)
        {
            _context = new();
            var roominformation = await _context.RoomInformations.FirstOrDefaultAsync(c => c.RoomId == roomID);
            if (roominformation == null) return null;
            return roominformation;
        }
        public async Task UpdateRoomInformation(RoomInformation roomInformation)
        {
            _context = new();
            try
            {
                // Retrieve the existing item
                var existingItem = await GetRoomInformation(roomInformation.RoomId);

                if (existingItem != null)
                {
                    // Check if the RoomID has changed
                    if (existingItem.RoomId != roomInformation.RoomId)
                    {
                        // Create a new RoomInformation with the new RoomID
                        var newRoomInformation = new RoomInformation
                        {
                            RoomId = roomInformation.RoomId,
                            RoomNumber = roomInformation.RoomNumber,
                            RoomTypeId = roomInformation.RoomTypeId,
                            RoomPricePerDate = roomInformation.RoomPricePerDate,
                            RoomDescription = roomInformation.RoomDescription,
                            RoomStatus = roomInformation.RoomStatus
                        };

                        // Remove the existing item
                        _context.RoomInformations.Remove(existingItem);

                        // Add the new item
                        _context.RoomInformations.Add(newRoomInformation);
                    }
                    else
                    {
                        // Update the existing item
                        _context.Entry(existingItem).CurrentValues.SetValues(roomInformation);
                    }
                }
                else
                {
                    // Add the new item if it doesn't exist
                    _context.RoomInformations.Add(roomInformation);
                }

                // Save changes to the database
                int changes = await _context.SaveChangesAsync();
                if (changes > 0)
                {
                    Console.WriteLine("Changes saved successfully to the database.");
                }
                else
                {
                    Console.WriteLine("No changes detected to save.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating the room information: {ex.Message}");
                throw; // Re-throw the exception to ensure any calling code is aware of the error
            }
        }



        public async Task DeleteRoomInformation(int roomID)
        {
            using var context = new FuminiHotelManagementContext();
            try
            {
                var roominformation = await context.RoomInformations.FindAsync(roomID);
                if (roominformation != null)
                {
                    context.RoomInformations.Remove(roominformation);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the room information: {ex.Message}");
                throw; // Re-throw the exception to ensure any calling code is aware of the error
            }

        }
    }
}
