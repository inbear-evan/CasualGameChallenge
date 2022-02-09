using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Yellotail
{
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField] private Slider progressBar;
        [SerializeField] private TMP_Text progressTxt;
        [SerializeField] private GameObject Loading;

        private float progressMax = 0.6f;

        public void Open(AsyncOperation asyncOperation)
        {
            asyncOperation.allowSceneActivation = false;
            StartCoroutine(CoProcess(asyncOperation));
        }

        IEnumerator CoProcess(AsyncOperation asyncOperation)
        {
            float ratio = 0.67f; // 0.6(progressMax)/0.9
            // 0%~60%
            while (asyncOperation.progress < 0.9f)
            {
                progressBar.value = asyncOperation.progress * ratio;

                yield return null;
            }

            asyncOperation.allowSceneActivation = true;

            // 61%~100%
            float fakeTime = 0.5f;
            float progressTime = 0f;
            while (progressTime < fakeTime)
            {
                progressTime += Time.deltaTime;

                progressBar.value = progressMax + (progressTime * 0.8f); // 0.6(progressMax) + (0.5 * 0.8) = 1
                progressTxt.text = $"{(int)(progressBar.value * 100)}%";

                yield return null;
            }
            Loading.SetActive(false);
        }
    }
}




