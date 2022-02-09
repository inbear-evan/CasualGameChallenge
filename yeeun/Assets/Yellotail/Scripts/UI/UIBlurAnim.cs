using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Yellotail.UI
{
    [RequireComponent(typeof(Image))]
    public class UIBlurAnim : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private UIBlurInfo start;
        [SerializeField] private UIBlurInfo end;

        private Material material;

        private void Awake()
        {
            GetComponent<Image>().material = new Material(Shader.Find("Yellotail/UI/Blur"));
            this.material = GetComponent<Image>().material;
        }

        private void OnEnable()
        {
            Play();
        }

        private void OnDisable()
        {
            Stop();
        }

        public void Play()
        {
            StartCoroutine(CoPlay(this.start, this.end));
        }

        public void PlayReverse()
        {
            StartCoroutine(CoPlay(this.end, this.start));
        }

        public void Stop()
        {
            StopCoroutine(nameof(CoPlay));
        }

        private IEnumerator CoPlay(UIBlurInfo start, UIBlurInfo end)
        {
            float progress = 0f;
 
            while (progress < 1)
            {
                var color = Color.Lerp(start.color, end.color, progress);
                var distortion = Mathf.Lerp(start.distortion, end.distortion, progress);

                this.material.SetColor("_Color", color);
                this.material.SetFloat("_Distortion", distortion);

                yield return new WaitForEndOfFrame();
                progress += Time.deltaTime / time;
            }

            this.material.SetColor("_Color", end.color);
            this.material.SetFloat("_Distortion", end.distortion);
        }
    }

    [System.Serializable]
    public struct UIBlurInfo
    {
        public float distortion;
        public Color color;
    }
}
