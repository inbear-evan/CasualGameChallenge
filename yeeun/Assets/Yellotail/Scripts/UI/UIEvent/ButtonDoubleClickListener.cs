using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Yellotail
{
    [RequireComponent(typeof(Button))]
    public class ButtonDoubleClickListener : 
        MonoBehaviour,
        IPointerClickHandler
    {
        [Tooltip("Max duration between 2 clicks in seconds")]
        [Range(0.01f, 0.5f)] 
        [SerializeField] private float doubleClickDuration = 0.4f;
        public UnityEvent onDoubleClick;

        private int clickCount = 0;
        private float elpasedTime = 0.0f;

        private Button button;

        private void Awake()
        {
            this.button = GetComponent<Button>();    
        }
        private void Update()
        {
            if (this.clickCount == 1)
            {
                this.elpasedTime += Time.deltaTime;
                if (this.elpasedTime > this.doubleClickDuration)
                {
                    this.clickCount = 0;
                    this.elpasedTime = 0.0f;
                }
            }
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            this.clickCount++;

            if (this.clickCount == 1)
            {
                this.elpasedTime = 0;
            }
            else if (clickCount > 1)
            {
                if (this.elpasedTime <= this.doubleClickDuration)
                {
                    this.clickCount = 0;
                    this.elpasedTime = 0.0f;

                    if (this.button.interactable)
                    {
                        onDoubleClick?.Invoke();
                    }
                }
            }
        }
    }
}
