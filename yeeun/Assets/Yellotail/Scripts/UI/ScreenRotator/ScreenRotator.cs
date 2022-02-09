using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Yellotail.InGame
{
    public class ScreenRotator : MonoBehaviour
    {
        public static ScreenRotator Instance;
        [SerializeField] private Vector2 portraitResolution;
        [SerializeField] private bool isLoadPortrait = false;

        private CanvasScaler[] scalers;
        private CanvasOrientationObject[] orientationObjects;

        public event System.Action<bool> OnChangedOrientation; // true = Portrait

        public static bool IsPortrait { get; private set; }
        public static bool IsLandscape => !IsPortrait;

        private void Awake()
        {

            Destroy(gameObject);
            scalers = FindObjectsOfType<CanvasScaler>();
            if (scalers == null)
            {
                Debug.LogError("RootScaler Not Exist");
                return;
            }

            var objects = new List<CanvasOrientationObject>();
            foreach (var scaler in scalers)
            {
                objects.AddRange(scaler.GetComponentsInChildren<CanvasOrientationObject>());
            }
            orientationObjects = objects.ToArray();

            if (isLoadPortrait)
                SetPortrait();
            else
                SetLandscape();
        }

        public void Change()
        {
            if (IsPortrait)
            {
                SetLandscape();
            }
            else
            {
                SetPortrait();
            }
        }

        public void SetPortrait()
        {
            IsPortrait = true;
            Screen.orientation = ScreenOrientation.Portrait;

            foreach (var scaler in scalers)
            {
                scaler.referenceResolution = portraitResolution;
                scaler.matchWidthOrHeight = 0f;
            }

            for (int i = 0; i < orientationObjects.Length; i++)
                orientationObjects[i].SetPortrait();

            OnChangedOrientation?.Invoke(true);
        }

        public void SetLandscape()
        {
            IsPortrait = false;
            Screen.orientation = ScreenOrientation.LandscapeLeft;

            foreach (var scaler in scalers)
            {
                scaler.referenceResolution = new Vector2(portraitResolution.y, portraitResolution.x);
                scaler.matchWidthOrHeight = 1f;
            }

            for (int i = 0; i < orientationObjects.Length; i++)
                orientationObjects[i].SetLandscape();

            OnChangedOrientation?.Invoke(false);
        }
    }
}
