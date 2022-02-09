using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail.Common
{
    public class RoomInfo
    {
        public int ID { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }

        public RoomInfo()
        {

        }

        public RoomInfo(int id, string name, int index)
        {
            this.ID = id;
            this.Name = name;
            this.Index = index;
        }
    }
}
