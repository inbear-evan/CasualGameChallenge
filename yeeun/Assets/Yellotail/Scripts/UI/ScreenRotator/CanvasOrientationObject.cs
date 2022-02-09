using UnityEngine;

namespace Yellotail.InGame
{
    [System.Serializable]
    public struct OrientationInfo
    {
        public Vector2 anchoredPosition;
        public Vector2 sizeDelta;
        public bool isActive;
    }

    public class CanvasOrientationObject : MonoBehaviour
    {
        [SerializeField] private OrientationInfo m_Portrait;
        [SerializeField] private OrientationInfo m_Landscape;
        [SerializeField] private bool m_IsActiveHandle = true;

        private RectTransform m_RectTransform = null;
        private RectTransform RectTransform
        {
            get
            {
                if (m_RectTransform == null)
                    m_RectTransform = GetComponent<RectTransform>();
                return m_RectTransform;
            }
        }

        public void SetPortrait()
        {
            RectTransform.anchoredPosition = m_Portrait.anchoredPosition;
            RectTransform.sizeDelta = m_Portrait.sizeDelta;

            if (m_IsActiveHandle)
                gameObject.SetActive(m_Portrait.isActive);
        }

        public void SetLandscape()
        {
            RectTransform.anchoredPosition = m_Landscape.anchoredPosition;
            RectTransform.sizeDelta = m_Landscape.sizeDelta;

            if (m_IsActiveHandle)
                gameObject.SetActive(m_Landscape.isActive);
        }

        public void SaveToPortirait()
        {
            m_Portrait.anchoredPosition = RectTransform.anchoredPosition;
            m_Portrait.sizeDelta = RectTransform.sizeDelta;
            m_Portrait.isActive = gameObject.activeSelf;
        }

        public void SaveToLandscape()
        {
            m_Landscape.anchoredPosition = RectTransform.anchoredPosition;
            m_Landscape.sizeDelta = RectTransform.sizeDelta;
            m_Landscape.isActive = gameObject.activeSelf;
        }
    }
}
