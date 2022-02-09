using System.Collections;
using TMPro;
using UnityEngine;

namespace Yellotail
{
    public class WrongPopup : MonoBehaviour
    {
        [SerializeField] private TMP_Text popupTxt;

        private bool isPoping = false;
        private Animation popFadeAnim;

        // Start is called before the first frame update
        void Start()
        {
            popupTxt.text = "";
            popFadeAnim = gameObject.GetComponent<Animation>();
        }

        public void Open(string txt)
        {
            if (!isPoping)
            {
                popupTxt.text = txt;
                StartCoroutine(nameof(FadeInAnim));
            }
        }

        IEnumerator FadeInAnim()
        {
            popFadeAnim.Play("PopupFade");
            isPoping = true;
            yield return new WaitForSeconds(4f);
            isPoping = false;
        }
    }
}
