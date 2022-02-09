using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Yellotail.UI.Toast
{
    public class Toast : SingletonBehaviour<Toast>
    {
        //public static Tost Instance;

        [SerializeField] private RectTransform csf;
        [SerializeField] private TMP_Text toastMessage;
        private bool isToasting = false;
        private Queue<string> message = new Queue<string>();
        private Animation fadeAnim;

        void Start()
        {
            fadeAnim = GetComponent<Animation>();
        }

        public void ShowMessage(string text)
        {
            gameObject.SetActive(true);
            message.Enqueue(text);
            // Debug.Log(message.Count);
            if (!isToasting)
            {
                isToasting = true;
                StartCoroutine(ShowYellotailToastMessage());
            }
        }

        IEnumerator ShowYellotailToastMessage()
        {
            while (message.Count != 0)
            {
                //Debug.Log("deQ " + message.Count);
                toastMessage.text = message.Dequeue();
                LayoutRebuilder.ForceRebuildLayoutImmediate(csf);
                fadeAnim.Play("ToastFade");
                yield return new WaitForSeconds(4f);
            }

            gameObject.SetActive(false);
            isToasting = false;
        }
    }
}

