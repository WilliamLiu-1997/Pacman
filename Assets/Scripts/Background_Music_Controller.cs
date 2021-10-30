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
    public GameObject GhostRed;
    public GameObject GhostPink;
    public GameObject GhostGreen;
    public GameObject GhostBlue;
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
            if (!my_Audio.isPlaying) playStartGhost();
        }
    }

    public float playStart()
    {
        my_Audio.clip = backgroundClip;
        my_Audio.loop = false;
        my_Audio.Play();
        return (float)my_Audio.clip.length;
    }

    public void Stop()
    {
        my_Audio.Stop();
    }

    public void playStartGhost()
    {
        my_Audio.Stop();
        my_Audio.clip = normalGhostClip;
        my_Audio.loop = true;
        my_Audio.Play();
    }
    public void playNormalGhost(int i)
    {
        if (my_Audio.clip != normalGhostClip)
        {
            my_Audio.Stop();
            my_Audio.clip = normalGhostClip;
            my_Audio.loop = true;
            my_Audio.Play();
        }
        if (i == 0) GhostRed.GetComponent<Ghost_Controller>().Normal();
        if (i == 1) GhostPink.GetComponent<Ghost_Controller>().Normal();
        if (i == 2) GhostGreen.GetComponent<Ghost_Controller>().Normal();
        if (i == 3) GhostBlue.GetComponent<Ghost_Controller>().Normal();
    }

    public void playScaredGhost(int i)
    {
        if (my_Audio.clip != scaredGhostClip)
        {
            my_Audio.Stop();
            my_Audio.clip = scaredGhostClip;
            my_Audio.loop = true;
            my_Audio.Play();
        }
        if (i == 0) GhostRed.GetComponent<Ghost_Controller>().Scare();
        if (i == 1) GhostPink.GetComponent<Ghost_Controller>().Scare();
        if (i == 2) GhostGreen.GetComponent<Ghost_Controller>().Scare();
        if (i == 3) GhostBlue.GetComponent<Ghost_Controller>().Scare();
    }
    public void playRecoverGhost(int i)
    {
        if (my_Audio.clip != scaredGhostClip)
        {
            my_Audio.Stop();
            my_Audio.clip = scaredGhostClip;
            my_Audio.loop = true;
            my_Audio.Play();
        }
        if (i == 0) GhostRed.GetComponent<Ghost_Controller>().Recover();
        if (i == 1) GhostPink.GetComponent<Ghost_Controller>().Recover();
        if (i == 2) GhostGreen.GetComponent<Ghost_Controller>().Recover();
        if (i == 3) GhostBlue.GetComponent<Ghost_Controller>().Recover();
    }

    public void playDeadGhost(int i)
    {
        if (my_Audio.clip != deadGhostClip)
        {
            my_Audio.Stop();
            my_Audio.clip = deadGhostClip;
            my_Audio.loop = true;
            my_Audio.Play();
        }
        if (i == 0) GhostRed.GetComponent<Ghost_Controller>().Die();
        if (i == 1) GhostPink.GetComponent<Ghost_Controller>().Die();
        if (i == 2) GhostGreen.GetComponent<Ghost_Controller>().Die();
        if (i == 3) GhostBlue.GetComponent<Ghost_Controller>().Die();
    }
    public void resetGhost(int i)
    {
        if (i == 0) GhostRed.GetComponent<Ghost_Controller>().Reset();
        if (i == 1) GhostPink.GetComponent<Ghost_Controller>().Reset();
        if (i == 2) GhostGreen.GetComponent<Ghost_Controller>().Reset();
        if (i == 3) GhostBlue.GetComponent<Ghost_Controller>().Reset();
        if (i == -1)
        {
            GhostRed.GetComponent<Ghost_Controller>().Reset();
            GhostPink.GetComponent<Ghost_Controller>().Reset();
            GhostGreen.GetComponent<Ghost_Controller>().Reset();
            GhostBlue.GetComponent<Ghost_Controller>().Reset();
        }
    }

    public float playDie()
    {
        my_Audio.clip = dieClip;
        my_Audio.loop = false;
        my_Audio.Play();
        return (float)my_Audio.clip.length;
    }
}
