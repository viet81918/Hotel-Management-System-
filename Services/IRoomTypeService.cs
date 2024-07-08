﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services
{
    public interface IRoomTypeService
    {
        RoomType GetRoomType(int roomTypeID);
        List<RoomType> GetAllRoomTypes();
        void AddRoomType(RoomType roomType);
        void UpdateRoomType(RoomType roomType);
        void DeleteRoomType(int roomTypeID);
    }
}
