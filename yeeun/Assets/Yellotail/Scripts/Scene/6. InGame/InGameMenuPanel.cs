using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Yellotail.InGame
{
    public class InGameMenuPanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup hamburgerLayout;
        [SerializeField] private TMP_Text peopleCount;

        public event Action OnRotateButtonClicked;
        public event Action OnExitButtonClicked;
        public event Action OnMessageButtonClicked;

        private void Start()
        {
            peopleCount.text = "1/8";
        }

        public void OnHamburgerButton()
        {
            // TO DO : Animation
            this.hamburgerLayout.gameObject.SetActive(!this.hamburgerLayout.gameObject.activeSelf);
        }

        public void OnCanlanderButton()
        {

        }

        public void OnFriendButton()
        {

        }

        public void OnSettingButton()
        {

        }

        public void OnExitButton()
        {
            OnExitButtonClicked?.Invoke();
        }

        public void OnRotateButton()
        {
            OnRotateButtonClicked?.Invoke();
        }

        public void OnMicButton()
        {

        }

        public void OnInventoryButton()
        {

        }

        public void OnCameraButton()
        {

        }

        public void OnMessageButton()
        {
            OnMessageButtonClicked?.Invoke();
        }
    }
}
