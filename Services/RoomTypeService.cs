using System.Collections.Generic;
using Repositories; // Assuming this namespace contains IRoomTypeRepository and RoomType
using BusinessObjects.Models; // Assuming this namespace contains RoomType

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

        public async Task<RoomType> GetRoomType(int roomTypeID)
        {
            return await roomTypeRepository.GetRoomType(roomTypeID);
        }

        public async Task<IEnumerable<RoomType>> GetAllRoomTypes()
        {
            return await roomTypeRepository.GetAllRoomTypes();
        }

        public async Task AddRoomType(RoomType roomType)
        {
           await  roomTypeRepository.AddRoomType(roomType);
        }

        public async Task UpdateRoomType(RoomType roomType)
        {
           await roomTypeRepository.UpdateRoomType(roomType);
        }

        public async Task DeleteRoomType(int roomTypeID)
        {
           await roomTypeRepository.DeleteRoomType(roomTypeID);
        }
    }
}
