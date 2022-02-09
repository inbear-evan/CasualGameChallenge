using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Yellotail
{
    public class ChatInputPopup : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;
        public event Action<string> OnSubmitted;

        private bool isSubmitted = false;

        void Start()
        {
            inputField.onSubmit.AddListener(OnSubmit);
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
            inputField.ActivateInputField();

            if(isSubmitted)
            {
                inputField.text = string.Empty;
                isSubmitted = false;
            }
        }

        public void OnSubmit(string text)
        {
            OnSubmitted?.Invoke(text);

            isSubmitted = true;
            OnClose();
        }

        public void OnClose()
        {
            gameObject.SetActive(false);
        }
    }
}
