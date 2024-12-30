using BusinessObjects.Models;
using DataAccessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoomInformationService : IRoomInformationService
    {
        private readonly IRoomInformationRepository roomInformationRepository;

        public RoomInformationService()
        {
            // Initialize repository; consider using dependency injection here
            roomInformationRepository = new RoomInformationRepository(); // Replace with actual repository implementation
        }

        public async Task AddRoomInformation(RoomInformation roomInformation)
        {
            await roomInformationRepository.AddRoomInformation(roomInformation);
        }

        public  async Task<IEnumerable<RoomInformation>> GetRoomInformations()
        {
            return await roomInformationRepository.GetRoomInformations();
        }

        public async Task<RoomInformation> GetRoomInformation(int roomID)
        {
            return await roomInformationRepository.GetRoomInformation(roomID);
        }

        public async Task UpdateRoomInformation(RoomInformation roomInformation)
        {
             await roomInformationRepository.UpdateRoomInformation(roomInformation);
        }

        public async Task DeleteRoomInformation(int roomID)
        {
           await roomInformationRepository.DeleteRoomInformation(roomID);
        }
    }
}
