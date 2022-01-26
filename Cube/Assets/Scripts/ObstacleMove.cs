using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public List<GameObject> obstacle;
    public float speed = 5;

    //public Transform movePosition;
    void Awake()
    {
        InitState();
    }
    void Update()
    {
        if(!GameManager.instance.isPause)
            transform.position += Vector3.back * speed * Time.deltaTime;
    }
    public void InitState()
    {
        for(int i = 0; i < obstacle.Count; i++)
        {
            InitColor(i);
            if (obstacle[i].activeSelf == false)
                obstacle[i].SetActive(true);
        }
    }
    Color RandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
    public void InitColor(int num)
    {
        obstacle[num].GetComponent<MeshRenderer>().material.color = RandomColor();
    }
}
