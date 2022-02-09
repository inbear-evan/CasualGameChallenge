using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Yellotail
{
    [RequireComponent(typeof(Button))]
    public class ButtonLongPressListener : 
        MonoBehaviour,
        IPointerDownHandler,
        IPointerUpHandler
    {
        [Tooltip("Hold duration in seconds")]
        [Range(0.3f, 5f)]
        [SerializeField] private float holdDuration = 0.5f;
        public UnityEvent onLongPress;

        private bool isPointerDown = false;
        private bool isLongPressed = false;
        private float elapsedTime = 0.0f;

        private Button button;
        private void Awake()
        {
            this.button = GetComponent<Button>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.isPointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            this.isPointerDown = false;
            this.isLongPressed = false;
            this.elapsedTime = 0.0f;
        }

        private void Update()
        {
            if (this.isPointerDown && !this.isLongPressed)
            {
                this.elapsedTime += Time.deltaTime;
                if (this.elapsedTime > this.holdDuration)
                {
                    this.isLongPressed = true;
                    this.elapsedTime = 0.0f;

                    if (this.button.interactable)
                    {
                        onLongPress?.Invoke();
                    }
                }
            }
        }
    }
}
