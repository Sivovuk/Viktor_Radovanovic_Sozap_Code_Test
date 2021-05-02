using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainMenu;
    public AudioSource mainGame;
    public AudioSource click;

    private static AudioManager instance;

    public static AudioManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayAudio(AudioSource audio) 
    {
        if (audio != null) 
        {
            audio.PlayOneShot(audio.clip);
        }
    }

    public void StopAudio(AudioSource audio)
    {
        if (audio != null)
        {
            audio.Stop();
        }
    }


    public void PlayClick1Sound()
    {
        PlayAudio(click);
    }

}
