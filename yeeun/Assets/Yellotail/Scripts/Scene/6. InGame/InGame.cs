using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Yellotail.InGame
{
    public class InGame : GameScene
    {
        [Header("[Game Load & Control]")]
        [SerializeField] private GameObject playerPrefab;

        [Header("[Bind Event]")]
        [SerializeField] private InGameMenuPanel menuPanel;
        [SerializeField] private ChattingPanel chattingPanel;
        [SerializeField] private ScreenRotator screenRotator;

        [SerializeField] private LoadingBar loadingBar;

        private string startLocationTag = "StartLocation";
        private GameObject[] startLocations;

        private void Awake()
        {
            BindEvent();
            StartCoroutine(LoadLevel());
        }


        private void InitStartLocations()
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(startLocationTag);
            startLocations = gameObjects;
        }

        private IEnumerator LoadLevel()
        {
            var levelIndex = GameManager.Instance.GameData.SelectedLevelIndex;
            var sceneIndex = (int)SceneUniqueID.LevelStartIndex + levelIndex;
            var asyncOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            asyncOperation.completed += OnSceneLoaded;
            loadingBar.Open(asyncOperation);
            yield return new WaitUntil(() => asyncOperation.isDone);
        }

        private void OnSceneLoaded(AsyncOperation asyncOp)
        {
            InitMyPlayer();
            asyncOp.completed -= OnSceneLoaded;
        }

        private void InitMyPlayer()
        {
            var player = Instantiate(this.playerPrefab);
            InitStartLocations();

            var startLocation = startLocations.RandomElement();
            if (startLocation != null)
            {
                var pt = player.transform;
                var st = startLocation.transform;
                pt.position = st.position;
                pt.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            }

            var myPlayer = player.AddComponent<MyPlayer>();
            myPlayer.nickname = GameManager.Instance.GameData.User.Name;
            chattingPanel.OnInputSubmitted += myPlayer.OnChatMessage;
            
            myPlayer.id = GameManager.Instance.GameData.User.Id;
        }

        private void BindEvent()
        {
            menuPanel.OnExitButtonClicked += () =>
            {
                screenRotator.SetPortrait();
                GoBack();
            };
            menuPanel.OnRotateButtonClicked += screenRotator.Change;
            menuPanel.OnMessageButtonClicked += chattingPanel.OpenInputFieldPopup;

            screenRotator.OnChangedOrientation += chattingPanel.OnChangedOrientation;
        }
    }
}
