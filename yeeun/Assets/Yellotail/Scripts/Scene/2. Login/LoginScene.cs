using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yellotail.Login;

namespace Yellotail
{
    public class LoginScene : GameScene
    {
        [SerializeField] private LoginPanel loginPanel;
        [SerializeField] private PermissionPanel permissionPanel;

        private void Awake()
        {
            this.loginPanel.OnLogined += () => permissionPanel.Open();
            this.permissionPanel.OnCompleted += GoNext; 
        }
    }
}