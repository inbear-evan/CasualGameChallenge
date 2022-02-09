using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Yellotail
{
    public class MyPlayer : Player
    {

        float delayTime = 3f;
        bool IsTalking = false;
        public override void OnChatMessage(string text)
        {
            //base.OnChatMessage(text);
            if (IsTalking) delayTime += delayTime;
            else delayTime = 3;
            Debug.Log(IsTalking);
            StartCoroutine(SpeechBubble(text, delayTime));

        }

        IEnumerator SpeechBubble(string text, float time)
        {
            TMP_Text myChat = this.GetComponentInChildren<TMP_Text>();
            myChat.enabled = true;
            Image bubbleImage = GetComponentInChildren<Image>();
            RectTransform csf = bubbleImage.GetComponent<ContentSizeFitter>().GetComponent<RectTransform>();

            myChat.text = text;
            LayoutRebuilder.ForceRebuildLayoutImmediate(csf);
            bubbleImage.enabled = true;
            IsTalking = true;
            yield return new WaitForSeconds(time);
            IsTalking = false;
            bubbleImage.enabled = false;

            myChat.text = String.Empty;


        }
    }

}
