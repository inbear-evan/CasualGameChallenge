using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static Yellotail.UltimateJoystick;

namespace Yellotail.UI
{
    [RequireComponent(typeof(Graphics))]
    public class UIScreenPos : UIBehaviour
    {
        [SerializeField] [Range(0f, 100f)] private float positionHorizontal; // 1~100 Range
        [SerializeField] [Range(0f, 100f)] private float positionVertical;
        [SerializeField] private ScalingAxis scalingAxis;
        [SerializeField] private Anchor anchor;
        [SerializeField] private float size;

        protected override void OnRectTransformDimensionsChange()
        {
            if (gameObject == null || !gameObject.activeInHierarchy)
                return;

            StartCoroutine(nameof(CoUpdatePosition));
        }

        public IEnumerator CoUpdatePosition()
        {
            yield return new WaitForEndOfFrame();
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            var rootCanvas = transform.root.GetComponent<RectTransform>();
            var rectTransform = GetComponent<RectTransform>();

            // Set the current reference size for scaling.
            float referenceSize = scalingAxis == ScalingAxis.Height ? rootCanvas.sizeDelta.y : rootCanvas.sizeDelta.x;

            // Configure the target size for this.
            float textureSize = referenceSize * (size / 10);

            // Configure the position that the user wants this to be located.
            Vector2 joystickPosition = new Vector2(rootCanvas.sizeDelta.x * (positionHorizontal / 100) - (textureSize * (positionHorizontal / 100)) + (textureSize / 2), rootCanvas.sizeDelta.y * (positionVertical / 100) - (textureSize * (positionVertical / 100)) + (textureSize / 2)) - (rootCanvas.sizeDelta / 2);

            if (anchor == Anchor.Right)
                joystickPosition.x = -joystickPosition.x;

            // Apply this size multiplied by the activation range.
            rectTransform.sizeDelta = new Vector2(textureSize, textureSize);

            // Apply the imagePosition.
            rectTransform.localPosition = joystickPosition;
        }
    }
}
