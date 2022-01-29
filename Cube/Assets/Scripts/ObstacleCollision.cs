using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public AudioSource BackMusic;
    public AudioSource JumpMusic;
    public AudioSource DeadMusic;
    public AudioSource SlideMusic;
    public AudioSource CrashMusic;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        Color color = this.GetComponent<MeshRenderer>().material.color;
        Color playerColor = other.GetComponent<MeshRenderer>().material.color;

        if (ObstaclesManager.instance.isActivedFever())
        {
            if(CrashMusic.isPlaying) CrashMusic.Stop();
            if (JumpMusic.isPlaying) JumpMusic.Stop();
            if(SlideMusic.isPlaying) SlideMusic.Stop();
            CrashMusic.Play();
            FeverTime.instance.Fever();
            FeverTime.instance.setColor(color);

            DebrisSpawner.instance.SpawnEffect(this.transform.position, color);
            GameManager.instance.addScore();
            this.gameObject.SetActive(false);
        }
        else if (playerColor == color)
        {
            if (CrashMusic.isPlaying) CrashMusic.Stop();
            if (JumpMusic.isPlaying) JumpMusic.Stop();
            if (SlideMusic.isPlaying) SlideMusic.Stop();
            CrashMusic.Play();
            DebrisSpawner.instance.SpawnEffect(this.transform.position, color);
            GameManager.instance.addScore();
            this.gameObject.SetActive(false);
        }
        else
        {
            if (BackMusic.isPlaying) BackMusic.Stop();
            if (JumpMusic.isPlaying) JumpMusic.Stop();
            if (SlideMusic.isPlaying) SlideMusic.Stop();
            if (CrashMusic.isPlaying) CrashMusic.Stop();
            DeadMusic.Play();
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
