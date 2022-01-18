using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] List<GameObject> obstacles;
    [SerializeField] Transform destObstacle;
    [SerializeField] Transform createObstacle;

    private void Start()
    {
        StartCoroutine(nameof(ObstacleMoveCheck));
    }

    IEnumerator ObstacleMoveCheck()
    {
        while (true) {
            for (int i = 0; i < obstacles.Count; i++)
            {
                if (obstacles[i].transform.position.z <= destObstacle.position.z)
                {
                    obstacles[i].transform.position = createObstacle.position;
                    obstacles[i].transform.GetComponent<ObstacleMove>().InitColor();
                    obstacles[i].transform.GetComponent<ObstacleMove>().speed += 0.1f;
                }
            }
            yield return null;
        }
    }
}
