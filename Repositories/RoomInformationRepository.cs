using BusinessObjects;
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
          

            public void AddRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.AddRoomInformation(roomInformation);

            public List<RoomInformation> GetRoomInformations() => RoomInformationDAO.GetRoomInformations();

            public RoomInformation GetRoomInformation(int roomID) => RoomInformationDAO.GetRoomInformation(roomID);

            public void UpdateRoomInformation(RoomInformation roomInformation) => RoomInformationDAO.UpdateRoomInformation(roomInformation);

            public void DeleteRoomInformation(int roomID) => RoomInformationDAO.DeleteRoomInformation(roomID);
        }
    }

