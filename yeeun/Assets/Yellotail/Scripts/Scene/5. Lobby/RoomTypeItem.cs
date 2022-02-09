using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Yellotail.Lobby
{
    public class RoomTypeItem : MonoBehaviour
    {
        [SerializeField] private Image thumnail;
        [SerializeField] private CanvasGroup toggleOn;
        [SerializeField] private TMP_Text toggleOffTitle;
        [SerializeField] private TMP_Text toggleOnTitle;

        public RectTransform Rect => rect;
        public Toggle Toggle => toggle;

        private RectTransform rect;
        private Toggle toggle;

        private const float FADE_TIME = 0.2f;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();  
            toggle = GetComponent<Toggle>();

            gameObject.SetActive(false);    
        }

        public void UpdateView(RoomTypeModel model)
        {
            toggleOffTitle.text = model.name;
            toggleOnTitle.text = model.name;
            // TO DO : thumnail
        }

        public void OnToggled(bool value)
        {
            float endValue = value ? 1f : 0f;
            toggleOn.DOFade(endValue, FADE_TIME);
        }

        public void OnDisableView()
        {
            
        }
    }
}
