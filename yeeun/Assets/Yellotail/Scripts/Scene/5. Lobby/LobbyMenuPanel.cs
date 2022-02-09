using UnityEngine;

namespace Yellotail.Lobby
{
    public class LobbyMenuPanel : MonoBehaviour
    {
        public event System.Action OnHomeClicked;
        public event System.Action OnCreateClicked;
        public event System.Action OnProfileClicked;

        public void OnToggledHome(bool value)
        {
            if (value)
            {
                this.OnHomeClicked?.Invoke();
            }
        }

        public void OnToggledCreateRoom(bool value)
        {
            if (value)
            {
                this.OnCreateClicked?.Invoke();
            }
        }

        public void OnToggledProfile(bool value)
        {
            if (value)
            {
                this.OnProfileClicked?.Invoke();
            }
        }
    }
}
