using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail.Common
{
    public class ItemInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ItemInfo()
        {

        }

        public ItemInfo(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
