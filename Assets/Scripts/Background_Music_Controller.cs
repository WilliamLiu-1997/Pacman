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

    public void playStartGhost()
    {
        my_Audio.Stop();
        my_Audio.clip = normalGhostClip;
        my_Audio.loop = true;
        my_Audio.Play();
    }
    public void playNormalGhost()
    {
        my_Audio.Stop();
        my_Audio.clip = normalGhostClip;
        my_Audio.loop = true;
        my_Audio.Play();
        GhostRed.GetComponent<Ghost_Controller>().Normal();
        GhostPink.GetComponent<Ghost_Controller>().Normal();
        GhostGreen.GetComponent<Ghost_Controller>().Normal();
        GhostBlue.GetComponent<Ghost_Controller>().Normal();
    }

    public void playScaredGhost()
    {
        my_Audio.Stop();
        my_Audio.clip = scaredGhostClip;
        my_Audio.loop = true;
        my_Audio.Play();
        GhostRed.GetComponent<Ghost_Controller>().Scare();
        GhostPink.GetComponent<Ghost_Controller>().Scare();
        GhostGreen.GetComponent<Ghost_Controller>().Scare();
        GhostBlue.GetComponent<Ghost_Controller>().Scare();
    }
    public void playRecoverGhost()
    {
        my_Audio.Stop();
        my_Audio.clip = scaredGhostClip;
        my_Audio.loop = true;
        my_Audio.Play();
        GhostRed.GetComponent<Ghost_Controller>().Recover();
        GhostPink.GetComponent<Ghost_Controller>().Recover();
        GhostGreen.GetComponent<Ghost_Controller>().Recover();
        GhostBlue.GetComponent<Ghost_Controller>().Recover();
    }

    public void playDeadGhost()
    {
        my_Audio.Stop();
        my_Audio.clip = deadGhostClip;
        my_Audio.loop = true;
        my_Audio.Play();
        GhostRed.GetComponent<Ghost_Controller>().Die();
        GhostPink.GetComponent<Ghost_Controller>().Die();
        GhostGreen.GetComponent<Ghost_Controller>().Die();
        GhostBlue.GetComponent<Ghost_Controller>().Die();
    }

    public float playDie()
    {
        my_Audio.clip = dieClip;
        my_Audio.loop = false;
        my_Audio.Play();
        return (float)my_Audio.clip.length;
    }
}
