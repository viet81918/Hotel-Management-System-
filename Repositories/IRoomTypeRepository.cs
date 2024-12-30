using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repositories
{
    public interface IRoomTypeRepository
    {
        Task<RoomType> GetRoomType(int roomTypeID);
        Task <IEnumerable<RoomType>> GetAllRoomTypes();
        Task AddRoomType(RoomType roomType);
        Task UpdateRoomType(RoomType roomType);
        Task DeleteRoomType(int roomTypeID);
    }
}
