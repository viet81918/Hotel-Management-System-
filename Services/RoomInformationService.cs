using BusinessObjects;
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

        public void AddRoomInformation(RoomInformation roomInformation)
        {
            roomInformationRepository.AddRoomInformation(roomInformation);
        }

        public List<RoomInformation> GetRoomInformations()
        {
            return roomInformationRepository.GetRoomInformations();
        }

        public RoomInformation GetRoomInformation(int roomID)
        {
            return roomInformationRepository.GetRoomInformation(roomID);
        }

        public void UpdateRoomInformation(RoomInformation roomInformation)
        {
            roomInformationRepository.UpdateRoomInformation(roomInformation);
        }

        public void DeleteRoomInformation(int roomID)
        {
            roomInformationRepository.DeleteRoomInformation(roomID);
        }
    }
}
