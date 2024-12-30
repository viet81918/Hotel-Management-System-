using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{

    public class RoomInformationRepository : IRoomInformationRepository
    {
        public async Task AddRoomInformation(RoomInformation roomInformation)
        {
           await RoomInformationDAO.Instance.AddRoomInformation(roomInformation);
        }

        public async Task DeleteRoomInformation(int roomID)
        {
            await RoomInformationDAO.Instance.DeleteRoomInformation(roomID);
        }

        public async Task<RoomInformation> GetRoomInformation(int roomID)
        {
            return await RoomInformationDAO.Instance.GetRoomInformation(roomID);
        }

        public async Task<IEnumerable<RoomInformation>> GetRoomInformations()
        {
           return await RoomInformationDAO.Instance.GetRoomInformations();
        }

        public async Task UpdateRoomInformation(RoomInformation roomInformation)
        {
            await RoomInformationDAO.Instance.UpdateRoomInformation(roomInformation);
        }
    }

}

