using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BusinessObjects.Models;
using Xceed.Wpf.Toolkit;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
 

    public class RoomTypeDAO : SingletonBase<RoomTypeDAO>
    {
        private   FuminiHotelManagementContext _context ; //loi singleton design partern factory
        public async Task<IEnumerable<RoomType>> GetAllRoomTypes()
        {
            _context = new();
            return await _context.RoomTypes.ToListAsync();
        }



        public async Task <RoomType> GetRoomType(int roomTypeID)
        {
            _context = new();
            var roomtype = await _context.RoomTypes.FirstOrDefaultAsync(c => c.RoomTypeId == roomTypeID);
            if (roomtype == null) return null;
            return roomtype;
        }

        // Add new RoomType
        public async Task AddRoomType(RoomType roomType)
        {
            _context = new();
            _context.RoomTypes.Add(roomType);
            await _context.SaveChangesAsync();
        }

        // Update existing RoomType
        public async Task UpdateRoomType(RoomType roomType)
        {
            _context = new();
            var existingItem = await GetRoomType(roomType.RoomTypeId);
            if (existingItem != null)
            {
                _context.Entry(existingItem).CurrentValues.SetValues(roomType);
            }
            else
            {
                _context.RoomTypes.Add(roomType);
            }
            await _context.SaveChangesAsync();
        }

        // Delete RoomType by ID
        public async Task DeleteRoomType(int roomTypeID)
        {
            _context = new();
            var roomtype = await GetRoomType(roomTypeID);
            if (roomtype != null)
            {
                _context.RoomTypes.Remove(roomtype);
                await _context.SaveChangesAsync();
            }

        }
    }
}

