using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    static public DebrisSpawner instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this.gameObject);
    }

    [SerializeField] GameObject Effect;
    [SerializeField] string tagName;
    public Material DebrisEffectMat;
    ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

    //private void FixedUpdate()
    //{
    //    objectPooler.SpawnFromPoolEffect(tagName, Vector3.zero, Quaternion.identity);
    //}
    public void SpawnEffect(Vector3 position, Color color)
    {
        DebrisEffectMat.color = color;
        objectPooler.SpawnFromPoolEffect(tagName, position, Quaternion.identity);
    }

}
