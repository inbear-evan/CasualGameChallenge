using UnityEngine;

namespace Yellotail
{
    public class Player : MonoBehaviour
    {
        private void Start()
        {

        }
        public string id { get; set; } = string.Empty;
        public string nickname { get; set; } = string.Empty;

        virtual public void OnChatMessage(string text)
        {
            Debug.Log(text);
        }

    }
}
