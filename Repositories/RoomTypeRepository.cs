using BusinessObjects;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public RoomType GetRoomType(int roomTypeID) => RoomTypeDAO.GetRoomType(roomTypeID);

        public List<RoomType> GetAllRoomTypes() => RoomTypeDAO.GetAllRoomTypes();

        public void AddRoomType(RoomType roomType) => RoomTypeDAO.AddRoomType(roomType);

        public void UpdateRoomType(RoomType roomType) => RoomTypeDAO.UpdateRoomType(roomType);

        public void DeleteRoomType(int roomTypeID) => RoomTypeDAO.DeleteRoomType(roomTypeID);
    }
}
