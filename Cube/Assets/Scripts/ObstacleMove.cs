using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public List<GameObject> obstacle;
    public float speed = 5;
    //public Transform movePosition;
    // Start is called before the first frame update
    void Start()
    {
        InitColor();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
    }

    Color RandomColor()
    {
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }
    public void InitColor()
    {
        obstacle[0].GetComponent<MeshRenderer>().material.color = RandomColor();
        obstacle[1].GetComponent<MeshRenderer>().material.color = RandomColor();
    }
}
