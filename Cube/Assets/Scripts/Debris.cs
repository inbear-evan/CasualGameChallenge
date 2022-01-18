using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] GameObject debri = null;
    [SerializeField] float speed = 0;
    [SerializeField] float exp = 100;
    [SerializeField] Vector3 offset = Vector3.zero;

    public void Explosion()
    {
        GameObject clone = Instantiate(debri, transform.position, Quaternion.identity);
        Rigidbody[] rb = clone.GetComponentsInChildren<Rigidbody>();
        for(int i = 0; i < rb.Length; i++)
        {
            rb[i].AddExplosionForce(speed, transform.position + offset, exp);
        }
        //transform.gameObject.SetActive(false);
    }
}
