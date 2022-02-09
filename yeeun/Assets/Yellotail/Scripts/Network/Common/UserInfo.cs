using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail.Common
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public UserInfo()
        {

        }

        public UserInfo(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
