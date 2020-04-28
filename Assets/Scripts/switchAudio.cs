using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchAudio : MonoBehaviour
{
    public AudioSource audiosrc;
    public AudioClip[] audioclipArray;
    private int nowPlaying = -1;

    // Start is called before the first frame update
    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
        playAudio(1);
    }

    public void playAudio(int i)
    {
        if (nowPlaying != i)
        {
            if (audiosrc.isPlaying)
            {
                audiosrc.Pause();
            }
            audiosrc.clip = audioclipArray[i];
            audiosrc.Play();
            nowPlaying = i;
        }
    }

    public void stopAudio()
    {
        audiosrc.Pause();
        nowPlaying = -1;
    }
}
