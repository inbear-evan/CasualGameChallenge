using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yellotail
{
    public class GameManager :
        SingletonBehaviour<GameManager>
    {
        public GameData GameData { get; private set; } = new GameData();

        public static long UnixTimeStamp => ((System.DateTimeOffset)System.DateTime.UtcNow).ToUnixTimeSeconds();

        [SerializeField] private ScreenOrientation startOrientation = ScreenOrientation.LandscapeLeft;
        public bool IsLandscape => Screen.orientation == ScreenOrientation.LandscapeLeft ||
            Screen.orientation == ScreenOrientation.LandscapeRight;

        public bool IsPortrait => Screen.orientation == ScreenOrientation.Portrait ||
            Screen.orientation == ScreenOrientation.PortraitUpsideDown;

        internal void LoadLevel(object index)
        {
            throw new NotImplementedException();
        }

        public void ToggleRotation()
        {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            switch (Screen.orientation)
            {
                case ScreenOrientation.LandscapeLeft:
                    Screen.orientation = ScreenOrientation.Portrait;
                    break;

                case ScreenOrientation.LandscapeRight:
                    Screen.orientation = ScreenOrientation.PortraitUpsideDown;
                    break;

                case ScreenOrientation.Portrait:
                    Screen.orientation = ScreenOrientation.LandscapeLeft;
                    break;

                case ScreenOrientation.PortraitUpsideDown:
                    Screen.orientation = ScreenOrientation.LandscapeRight;
                    break;
            }
#endif
        }

        private void Awake()
        {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            Screen.orientation = this.startOrientation;   
#endif
        }

        public void LoadLevel(int levelIndex)
        {
            GameManager.Instance.GameData.SelectedLevelIndex = levelIndex;
            SceneManager.LoadSceneAsync((int)SceneUniqueID.InGame, LoadSceneMode.Single);
        }
    }
}
