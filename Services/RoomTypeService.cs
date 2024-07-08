using System.Collections.Generic;
using Repositories; // Assuming this namespace contains IRoomTypeRepository and RoomType
using BusinessObjects; // Assuming this namespace contains RoomType

namespace Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository roomTypeRepository;

        public RoomTypeService()
        {
            // Initialize repository; consider using dependency injection here
            roomTypeRepository = new RoomTypeRepository(); // Replace with actual repository implementation
        }

        public RoomType GetRoomType(int roomTypeID)
        {
            return roomTypeRepository.GetRoomType(roomTypeID);
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return roomTypeRepository.GetAllRoomTypes();
        }

        public void AddRoomType(RoomType roomType)
        {
            roomTypeRepository.AddRoomType(roomType);
        }

        public void UpdateRoomType(RoomType roomType)
        {
            roomTypeRepository.UpdateRoomType(roomType);
        }

        public void DeleteRoomType(int roomTypeID)
        {
            roomTypeRepository.DeleteRoomType(roomTypeID);
        }
    }
}
