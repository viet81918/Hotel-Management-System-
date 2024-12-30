using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoomInformationRepository
    {
      Task AddRoomInformation(RoomInformation roomInformation);
        Task<IEnumerable <RoomInformation>> GetRoomInformations();
        Task<RoomInformation> GetRoomInformation(int roomID);
        Task UpdateRoomInformation(RoomInformation roomInformation);
        Task DeleteRoomInformation(int roomID);
    }
}
