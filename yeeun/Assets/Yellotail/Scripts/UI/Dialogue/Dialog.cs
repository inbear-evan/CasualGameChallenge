using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Yellotail.UI.Dialog
{
    public enum DialogResult
    {
        OK,
        Cancel
    }

    public enum DialogType
    {
        OK,
        OKCancel
    }

    public class Dialog : SingletonBehaviour<Dialog>, IDialogue
    {
        [SerializeField] private UIBlurAnim m_BlurAnim;
        [SerializeField] private Animator m_DialogBox;

        [SerializeField] private TMP_Text m_Title;
        [SerializeField] private TMP_Text m_Content;
        [SerializeField] private TMP_Text m_OK;
        [SerializeField] private TMP_Text m_Cancel;

        [SerializeField] private Button m_OKButton;
        [SerializeField] private Button m_CancelButton;

        private DialogType m_Type;
        public DialogType Type
        {
            get { return m_Type; }
            set
            {
                m_Type = value;

                if (m_Type == DialogType.OK)
                {
                    m_OKButton.gameObject.SetActive(true);
                    m_CancelButton.gameObject.SetActive(false);
                }
                else if (m_Type == DialogType.OKCancel)
                {
                    m_OKButton.gameObject.SetActive(true);
                    m_CancelButton.gameObject.SetActive(true);
                }
                else
                {
                    throw new System.NotImplementedException();
                }
            }
        }

        public string Title { get { return m_Title.text; } set { m_Title.text = value; } }
        public string Content { get { return m_Content.text; } set { m_Content.text = value; } }
        public string OK { get { return m_OK.text; } set { m_OK.text = value; } }
        public string Cancel { get { return m_Cancel.text; } set { m_Cancel.text = value; } }

        public HorizontalAlignmentOptions ContentAlignment { get { return m_Content.horizontalAlignment; } set { m_Content.horizontalAlignment = value; } }

        public event Action<DialogResult> OnResulted;

        private void Awake()
        {
            if (Instance)
            {
                ContentAlignment = HorizontalAlignmentOptions.Center;
                gameObject.SetActive(false);
            }
        }

        public void Open()
        {
            m_DialogBox.SetBool("Close", false);
            gameObject.SetActive(true);
        }

        public void Close()
        {
            StartCoroutine(CoClose());
        }

        IEnumerator CoClose()
        {
            m_DialogBox.SetBool("Close", true);
            m_BlurAnim.PlayReverse();

            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }

        public void OnOK()
        {
            OnResulted?.Invoke(DialogResult.OK);
            OnResulted = null;
            Close();
        }

        public void OnCancel()
        {
            OnResulted?.Invoke(DialogResult.Cancel);
            OnResulted = null;
            Close();
        }
    }
}
