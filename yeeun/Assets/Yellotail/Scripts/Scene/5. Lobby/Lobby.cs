using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yellotail.Lobby
{
    public class Lobby : GameScene
    {
        [SerializeField] LobbyMenuPanel menu;

        [SerializeField] HomePanel home;
        [SerializeField] CreateRoomPanel createRoomPanel;

        private void Awake()
        {
            this.menu.OnHomeClicked += () => { Debug.Log("Home Toggled"); };
            this.menu.OnCreateClicked += () => { this.createRoomPanel.Open(); };
            this.menu.OnProfileClicked += () => { Debug.Log("Profile Toggled"); };    
        }
    }
}
