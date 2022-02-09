using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    [System.Serializable]
    public struct LevelInformation
    {
        public string Name;
        public string DisplayName;
        public int ID;
        public Sprite Preview;
    }

    [CreateAssetMenu(fileName = "Level Data", menuName = "¡Ú Yellotail/Data Tables/Level Data", order =int.MaxValue)]
    public class LevelTable : ScriptableObject
    {
        [SerializeField] private LevelInformation[] levelInformations;   
    }
}
