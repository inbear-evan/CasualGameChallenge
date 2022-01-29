using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource BackMusic;
    public AudioSource JumpMusic;
    public AudioSource DeadMusic;
    public AudioSource SlideMusic;
    public AudioSource CrashMusic;
    public Slider musicControl;

    // Start is called before the first frame update
    void Start()
    {
        BackMusic.Play();
        JumpMusic.Pause();
        DeadMusic.Pause();
        SlideMusic.Pause();
        CrashMusic.Pause();
    }

    private void FixedUpdate()
    {
        BackMusic.volume = musicControl.value;
        JumpMusic.volume = musicControl.value;
        DeadMusic.volume = musicControl.value;
        SlideMusic.volume = musicControl.value;
        CrashMusic.volume = musicControl.value;
    }
}
