using UnityEngine;

namespace Yellotail.Login
{
    public class PermissionPanel : MonoBehaviour
    {
        public event System.Action OnCompleted;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void OnOkButton()
        {
            OnCompleted?.Invoke();
        }
    }
}
