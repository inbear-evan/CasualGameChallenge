using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {   
        if(collision.gameObject.GetComponent<Player>() != null)
            collision.gameObject.GetComponent<Player>().toggleGround();    
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
            collision.gameObject.GetComponent<Player>().toggleGround();
    }
}
