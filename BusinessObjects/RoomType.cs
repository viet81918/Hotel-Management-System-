namespace BusinessObjects
{
    public class RoomType
    {
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public string TypeDescription { get; set; }
        public string TypeNote { get; set; }

        // Parameterized Constructor
        public RoomType(int roomTypeID, string roomTypeName, string typeDescription, string typeNote)
        {
            RoomTypeID = roomTypeID;
            RoomTypeName = roomTypeName;
            TypeDescription = typeDescription;
            TypeNote = typeNote;
        }

        // Default Constructor (optional)
        public RoomType()
        {
        }
    }
}
