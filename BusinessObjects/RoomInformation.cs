namespace BusinessObjects
{
    public class RoomInformation
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public int RoomStatus { get; set; }
        public double RoomPricePerDate { get; set; }
        public int RoomTypeID { get; set; }

        // Parameterized Constructor
        public RoomInformation(int roomID, string roomNumber, string roomDescription, int roomMaxCapacity, int roomStatus, double roomPricePerDate, int roomTypeID)
        {
            RoomID = roomID;
            RoomNumber = roomNumber;
            RoomDescription = roomDescription;
            RoomMaxCapacity = roomMaxCapacity;
            RoomStatus = roomStatus;
            RoomPricePerDate = roomPricePerDate;
            RoomTypeID = roomTypeID;
        }

        // Default Constructor (optional)
        public RoomInformation()
        {
        }
    }

}
