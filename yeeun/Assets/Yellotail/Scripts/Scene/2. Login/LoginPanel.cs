using System;
using System.Collections;
using System.Text.RegularExpressions;
using Nettention.Proud;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yellotail.UI.Toast;

namespace Yellotail.Login
{
    public class LoginPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField id;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private WrongPopup wrongPopup;
        [SerializeField] private Button loginButton;


        public event System.Action OnLogined;

        Regex regexPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\W]).{4,20}$");
        Regex regexID = new Regex(@"^[0-9a-zA-Z]{4,20}$");

        private void Start()
        {
            this.loginButton.interactable = false;

            this.id.onSubmit.AddListener(OnSubmitId);
            this.password.onSubmit.AddListener(OnSubmitPassword);

            NetworkManager.Instance.ConnectToServer();
        }

        private void OnEnable()
        {
            NetworkManager.Instance.OnConnected.AddListener(OnConnectedHandler);
            NetworkManager.Instance.OnLogined.AddListener(OnLoginedHandler);
        }

        private void OnDisable()
        {
            NetworkManager.Instance.OnConnected.RemoveListener(OnConnectedHandler);
            NetworkManager.Instance.OnLogined.RemoveListener(OnLoginedHandler);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                id.text = "yellotail";
                password.text = "Yellotail0@";
            }
        }
#endif
        public void OnSubmitId(string value)
        {
            this.password.ActivateInputField();
            this.password.Select();
        }

        public void OnSubmitPassword(string value)
        {

        }

        public void OnLogin()
        {

            if (string.IsNullOrEmpty(id.text))
            {
                wrongPopup.Open("아이디를 입력해주세요.");
                return;
            }

            if (string.IsNullOrEmpty(password.text))
            {
                wrongPopup.Open("비밀번호를 입력해주세요.");
                return;
            }

#if !UNITY_EDITOR
            if (!regexID.IsMatch(id.text) || !regexPassword.IsMatch(password.text))
            {
                wrongPopup.Open("아이디 혹은 비밀번호가 맞지 않습니다.");
                return;
            }
#endif

            NetworkManager.Instance.Login(id.text, password.text);
        }

        private void OnConnectedHandler(ErrorType errorType)
        {
            if (errorType == ErrorType.Ok)
            {
                this.loginButton.interactable = true;
                Debug.Log($"<color=lime>Success to join server</color>");
            }
            else
            {
                Debug.LogError($"Failed to join server! ({errorType})");
            }
        }


        private void OnLoginedHandler(int result)
        {
            Debug.Log($"<color=tomato>OnResponseLogin: {result} </color>");
            if (result == 0)
            {
                Toast.Instance.ShowMessage("환영합니다");
                OnLogined?.Invoke();
            }
            else
            {
                //TODO 로그인 실패 메시지 출력
            }
        }

        public void OnNaverLogin()
        {

        }

        public void OnForgotPassword()
        {

        }
    }
}

