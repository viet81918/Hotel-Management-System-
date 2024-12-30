using BusinessObjects.Models;
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
        public async Task AddRoomType(RoomType roomType)
        {
          await RoomTypeDAO.Instance.AddRoomType(roomType);
        }

        public async Task DeleteRoomType(int roomTypeID)
        {
            await RoomTypeDAO.Instance.DeleteRoomType(roomTypeID);
        }

        public async Task<IEnumerable<RoomType>> GetAllRoomTypes()
        {
           return await RoomTypeDAO.Instance.GetAllRoomTypes();
        }

        public async Task<RoomType> GetRoomType(int roomTypeID)
        {
            return await RoomTypeDAO.Instance.GetRoomType(roomTypeID);
        }

        public async Task UpdateRoomType(RoomType roomType)
        {
            await RoomTypeDAO.Instance.UpdateRoomType(roomType);
        }
    }
}
