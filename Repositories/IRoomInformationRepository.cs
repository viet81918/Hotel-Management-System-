using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoomInformationRepository
    {
        void AddRoomInformation(RoomInformation roomInformation);
        List<RoomInformation> GetRoomInformations();
        RoomInformation GetRoomInformation(int roomID);
        void UpdateRoomInformation(RoomInformation roomInformation);
        void DeleteRoomInformation(int roomID);
    }
}
