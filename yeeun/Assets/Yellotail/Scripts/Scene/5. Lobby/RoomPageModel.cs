using System.Collections.Generic;

namespace Yellotail.Lobby
{
    public class RoomPageModel
    {
        public List<RoomInfo> rooms = new List<RoomInfo>();
    }

    public class RoomInfo
    {
        public string title;
        public string master;
        public string thumnail;
        public int index;
    }
}
