using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    [System.Serializable]
    public struct AssetBundleContent
    {
        public string name;
        public Hash128 hash;
        public int crc;        
    }
}
