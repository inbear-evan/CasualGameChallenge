using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstaclesManager : MonoBehaviour
{
    static public ObstaclesManager instance;
    private void Awake()
    {
        if(instance == null) instance = this;
        else if(instance != this) Destroy(this.gameObject);
    }

    [SerializeField] List<GameObject> obstacles;
    [SerializeField] Transform destObstacle;
    [SerializeField] Transform createObstacle;
    [SerializeField] Transform originObstacle;
    [SerializeField] Transform feverCreateObstacle;
    [SerializeField] GameObject feverObstacles;
    [SerializeField] Player player;
    
    public float feverSpeed = 10;
    public float speed = 5;
    float speedBuf;
    bool feverSpeedChanged = true;
    private void Start()
    {
        setSpeed(speed);
        StartCoroutine(nameof(ObstacleMoveCheck));
    }

    void InitObstacle()
    {
        obstacles[0].transform.position = originObstacle.position;
        obstacles[1].transform.position = feverCreateObstacle.position;
        obstacles[2].transform.position = createObstacle.position;

        obstacles[0].transform.GetComponent<ObstacleMove>().InitState();
        obstacles[1].transform.GetComponent<ObstacleMove>().InitState();
        obstacles[2].transform.GetComponent<ObstacleMove>().InitState();
    }

    public void activeFever()
    {
        obstacles[0].transform.parent.gameObject.SetActive(false);
        feverObstacles.SetActive(true);
    }

    public void inActiveFever()
    {
        obstacles[0].transform.parent.gameObject.SetActive(true);
        feverObstacles.SetActive(false);
        InitObstacle();
       
    }
    public bool isActivedFever()
    {
        return feverObstacles.activeSelf;
    }
    bool setFeverSpeed(float speed)
    {
        for (int i = 1; i < feverObstacles.transform.childCount; i++)
        {
            feverObstacles.transform.GetChild(i).GetComponent<ObstacleMove>().speed = speed;
        }
        return false;
    }
    void setSpeed(float speed)
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].transform.GetComponent<ObstacleMove>().speed = speed;
        }
    }
    IEnumerator ObstacleMoveCheck()
    {
        while (true) {
            if (feverObstacles.activeSelf)
            {
                if (feverSpeedChanged)
                {
                    feverSpeedChanged = setFeverSpeed(feverSpeed);
                }
                for (int i = 1; i < feverObstacles.transform.childCount; i++)
                {
                    if (feverObstacles.transform.GetChild(i).position.z <= destObstacle.position.z)
                    {
                        feverObstacles.transform.GetChild(i).position = feverCreateObstacle.position;
                        feverObstacles.transform.GetChild(i).GetComponent<ObstacleMove>().InitState();
                    }
                }
            }
            else
            {
                for (int i = 0; i < obstacles.Count; i++)
                {
                    if (obstacles[i].transform.position.z <= destObstacle.position.z)
                    {
                        obstacles[i].transform.position = createObstacle.position;
                        obstacles[i].transform.GetComponent<ObstacleMove>().InitState();
                        obstacles[i].transform.GetComponent<ObstacleMove>().speed += 0.1f;
                        speedBuf = obstacles[i].transform.GetComponent<ObstacleMove>().speed;
                        player.playerColorChange();
                    }
                }
                
            }
            yield return null;
        }
    }
}

