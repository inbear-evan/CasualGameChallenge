using System.Collections;
using System.Collections.Generic;
using Nettention.Proud;
using UnityEngine;

namespace Yellotail
{
    [System.Serializable]
    public class PlayerInfo
    {
        public HostID ID { get; set; }
        public Gender Gender { get; set; }
        public string Nickname { get; set; }
        public PlayerParts Parts { get; set; }
    }
}
