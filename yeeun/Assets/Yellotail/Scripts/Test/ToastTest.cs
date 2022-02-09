using System.Collections.Generic;
using UnityEngine;
using Yellotail.UI.Toast;

namespace Yellotail
{
    public class ToastTest : MonoBehaviour
    {
        [SerializeField] private List<string> messages;
        [SerializeField] private Toast yellotailToast;
        private int prevIndex = 0;

        public void OnAndroidToast()
        {
            string message = GetRandomMessage();
            ShowAndroidToastMessage(message);
        }

        public void OnYellotailToast()
        {
            string message = GetRandomMessage();
            yellotailToast.ShowMessage(message);
            //StartCoroutine(yellotailToast.ShowYellotailToastMessage());
        }

        private string GetRandomMessage()
        {
            int index;
            do
            {
                index = Random.Range(0, messages.Count);
            }
            while (index == prevIndex);

            prevIndex = index;

            return messages[index];
        }

        public void ShowAndroidToastMessage(string message)
        {
#if UNITY_STANDALONE || UNITY_EDITOR || UNITY_WEBGL
            Debug.Log("Toast : " + message);
#elif UNITY_IOS
        // IOS ...
#elif UNITY_ANDROID
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
            AndroidJavaClass toastClass = new AndroidJavaClass ("android.widget.Toast");

            activity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject> ("makeText", activity,
                    message, 0);
                toastObject.Call ("show");
            }));
        }
#endif
        }
    }
}
