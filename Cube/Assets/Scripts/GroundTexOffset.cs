using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTexOffset : MonoBehaviour
{
    Material material;
    public ObstacleMove obsMove;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        material.mainTextureOffset -= new Vector2(0, (obsMove.speed * Time.deltaTime)/10f);
    }
}
