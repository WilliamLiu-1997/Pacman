using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Music_Controller : MonoBehaviour
{
    public AudioClip backgroundClip;

    public AudioClip normalGhostClip;

    public AudioClip scaredGhostClip;

    public AudioClip deadGhostClip;

    public AudioClip dieClip;
    public bool Started;

    AudioSource my_Audio;


    // Start is called before the first frame update
    void Start()
    {
        my_Audio = GetComponent<AudioSource>();
        Started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Started)
        {
            if (!my_Audio.isPlaying) playNormalGhost();
        }
    }

    public float playStart()
    {
        my_Audio.clip = backgroundClip;
        my_Audio.loop = false;
        my_Audio.Play();
        return (float)my_Audio.clip.length;
    }

    public void playNormalGhost()
    {
        my_Audio.clip = normalGhostClip;
        my_Audio.loop = true;
        my_Audio.Play();
    }

    public void playScaredGhost()
    {
        my_Audio.clip = scaredGhostClip;
        my_Audio.loop = true;
        my_Audio.Play();
    }

    public void playDeadGhost()
    {
        my_Audio.clip = deadGhostClip;
        my_Audio.loop = true;
        my_Audio.Play();
    }

    public float playDie()
    {
        my_Audio.clip = dieClip;
        my_Audio.loop = false;
        my_Audio.Play();
        return (float)my_Audio.clip.length;
    }
}
