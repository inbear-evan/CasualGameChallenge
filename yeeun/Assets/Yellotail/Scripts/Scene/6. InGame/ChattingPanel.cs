using UnityEngine;
using DG.Tweening;
using System;
using TMPro;
using System.Collections;
using UnityEngine.UI;

namespace Yellotail.InGame
{
    public class ChattingPanel : MonoBehaviour
    {
        [SerializeField] GameObject chatButton;
        [SerializeField] ChatList chatList;
        [SerializeField] RectTransform chatScrollView;
        [SerializeField] ChatInputPopup inputfieldPopup;

        public event Action<string> OnInputSubmitted;
        private const float animTime = 0.25f;

        private void Start()
        {
            OnInputSubmitted += AddMyChat;
            inputfieldPopup.OnSubmitted += OnInputSubmitted;

            MoveChatListPosition(false, isAnim: false);
        }

        public void OnChatButton()
        {
            bool isShow = chatButton.activeSelf;

            MoveChatListPosition(isShow, true);
            chatButton.SetActive(!isShow);
        }

        private void MoveChatListPosition(bool isShow, bool isAnim)
        {
            float startX = 0f, endX = 0f;
            if (isShow)
            {
                startX = -chatScrollView.rect.width;
            }
            else
            {
                endX = -chatScrollView.rect.width;
            }

            if (isAnim)
            {
                chatScrollView.anchoredPosition = new Vector2(startX, chatScrollView.anchoredPosition.y);
                chatScrollView.DOAnchorPosX(endX, animTime);
            }
            else
            {
                chatScrollView.anchoredPosition = new Vector2(endX, chatScrollView.anchoredPosition.y);
            }
        }

        public void OnChangedOrientation(bool isPortrait)
        {
            bool isChatHiding = chatButton.activeSelf;
            if (isChatHiding)
            {
                MoveChatListPosition(false, isAnim: false);
            }
        }

        public void OpenInputFieldPopup()
        {
            inputfieldPopup.Open();
        }


        private void AddMyChat(string text)
        {
            var user = GameManager.Instance.GameData.User.Name;
            var chat = chatList.GetChatModel(ChatType.Message, user, text);
            Debug.Log(text);

        }
    }
}
