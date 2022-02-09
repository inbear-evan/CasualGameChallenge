using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Yellotail
{
    public class UltimateAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private string actionName;

        private bool isPressed;
        public bool IsPressed => isPressed;

        private void Awake()
        {
            if (Application.isPlaying && !string.IsNullOrEmpty(actionName))
            {
                if (UltimateActions.ContainsKey(actionName))
                    UltimateActions.Remove(actionName);

                // Then register the action
                UltimateActions.Add(actionName, this);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            this.isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            this.isPressed = false;
        }


        private static readonly Dictionary<string, UltimateAction> UltimateActions = new Dictionary<string, UltimateAction>();
        private static bool ActionConfirmed(string actionName)
        {
            if (!UltimateActions.ContainsKey(actionName))
            {
                Debug.LogWarning("Ultimate Actions\nNo Ultimate Action has been registered with the name: " + actionName + ".");
                return false;
            }

            return true;
        }

        public static bool IsActionPressed(string actionName)
        {
            if (!ActionConfirmed(actionName))
                return false;

            return UltimateActions[actionName].IsPressed;
        }
    }
}
