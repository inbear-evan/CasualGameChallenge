using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Yellotail
{
    [System.Serializable]
    public class GameData
    {
        public GameData()
        {

        }

        public int SelectedLevelIndex { get; set; } = 0;
        public UserData User { get; set; } = new UserData();

        public PlayerInfo PlayerInfo = new PlayerInfo();
    }

    [System.Serializable]
    public class UserData
    {

        public UserData()
        {

        }
          

        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
       // public TMP_Text Chat { get; set; } 
    }
}
