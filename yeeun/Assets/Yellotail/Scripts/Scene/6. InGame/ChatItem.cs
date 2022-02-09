using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Yellotail.InGame
{
    public class ChatItem : MonoBehaviour
    {
        [SerializeField] TMP_Text label;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void UpdateView(ChatModel model)
        {
            label.text = model.message;
            label.color = model.color;
        }
    }
}
