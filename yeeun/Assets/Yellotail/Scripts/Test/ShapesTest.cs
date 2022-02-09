using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yellotail.InGame;

namespace Yellotail
{
    public class ShapesTest : MonoBehaviour
    {
        [Header("[Game Load & Control]")]
        [SerializeField] private GameObject playerPrefab;

        private string startLocationTag = "StartLocation";
        private GameObject[] startLocations;

        [Header("[Navigation]")]
        [SerializeField] private Vector3 naviPosition;
        private Transform player;

        private void Awake()
        {
            GameManager.Instance.GameData.SelectedLevelIndex = 0;
            StartCoroutine(LoadLevel());
        }

        private void InitStartLocations()
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(this.startLocationTag);
            this.startLocations = gameObjects;
        }

        private IEnumerator LoadLevel()
        {
            var levelIndex = GameManager.Instance.GameData.SelectedLevelIndex;
            var sceneIndex = (int)SceneUniqueID.LevelStartIndex + levelIndex;
            var asyncOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            asyncOperation.completed += OnSceneLoaded;
            yield return new WaitUntil(() => asyncOperation.isDone);
        }

        private void OnSceneLoaded(AsyncOperation asyncOp)
        {
            player = Instantiate(this.playerPrefab).transform;

            InitStartLocations();

            var startLocation = this.startLocations.RandomElement();
            if (startLocation != null)
            {
                var st = startLocation.transform;
                player.position = st.position;
                player.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            }

            asyncOp.completed -= OnSceneLoaded;
        }

        public void OnNavigationToggle(bool isOn)
        {
            Debug.Log("*** " + isOn);
        }
    }
}
