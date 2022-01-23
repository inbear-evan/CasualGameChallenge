using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        Color color = this.GetComponent<MeshRenderer>().material.color;
        Color playerColor = other.GetComponent<MeshRenderer>().material.color;

        if (ObstaclesManager.instance.isActivedFever())
        {
            FeverTime.instance.Fever();
            FeverTime.instance.setColor(color);

            DebrisSpawner.instance.SpawnEffect(this.transform.position, color);
            GameManager.instance.addScore();
            this.gameObject.SetActive(false);
        }
        else if (playerColor == color)
        {
            DebrisSpawner.instance.SpawnEffect(this.transform.position, color);
            GameManager.instance.addScore();
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("GameOver");
            other.gameObject.SetActive(false);
            GameManager.instance.GameOver();
            DebrisSpawner.instance.SpawnEffect(other.transform.position, playerColor);
        }
        //other.GetComponent<MeshRenderer>().material.color = GameManager.instance.setPlayerColor();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //}
}
