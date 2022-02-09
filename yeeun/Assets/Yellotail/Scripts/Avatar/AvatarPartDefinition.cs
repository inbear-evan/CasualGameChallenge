using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    [System.Serializable]
    public class AvatarPartDefinition
    {
        public AvatarPart part;
        public GameObject prefab;
        public Transform partInstance;
    }
}
