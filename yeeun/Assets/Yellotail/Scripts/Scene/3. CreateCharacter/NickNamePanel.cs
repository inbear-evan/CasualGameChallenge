using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace Yellotail
{
    public class NickNamePanel : MonoBehaviour
    {
        public event Action OnNextClicked;

        [SerializeField] GameObject nextButton;
        [SerializeField] TMP_InputField inputField;

        RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                inputField.text = "옐로테일";
            }
        }
#endif
        public void Open()
        {
            rectTransform.DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.Linear);
        }
        public void OnNext()
        {
            GameManager.Instance.GameData.User.Name = inputField.text;
            GameManager.Instance.GameData.User.Description = ("옐로테일 메타버스 프로젝트에  " + "\n오신걸 환영합니다");
            this.OnNextClicked?.Invoke();
        }
        public void OnGoLeft()
        {
            rectTransform.DOAnchorPos(new Vector2(0, 0), 0.5f).SetEase(Ease.Linear);
        }
        public void OnGoBack()
        {
            rectTransform.DOAnchorPos(new Vector2(1080, 0), 0.5f).SetEase(Ease.Linear);
        }
        public void OnNicknameChanged(string value)
        {
            bool isStringExist = !string.IsNullOrEmpty(value);
            this.nextButton.SetActive(isStringExist);
        }
    }
}

