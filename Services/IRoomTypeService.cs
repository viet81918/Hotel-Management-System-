using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services
{
    public interface IRoomTypeService
    {
        Task<RoomType> GetRoomType(int roomTypeID);
       Task<IEnumerable<RoomType>> GetAllRoomTypes();
        Task AddRoomType(RoomType roomType);
        Task UpdateRoomType(RoomType roomType);
        Task DeleteRoomType(int roomTypeID);
    }
}
