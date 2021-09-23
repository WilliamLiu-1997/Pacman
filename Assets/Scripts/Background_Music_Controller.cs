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

    public AudioSource audio;

    private bool intro_done = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = backgroundClip;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        intro_done = true;
        playNormalGhost();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playNormalGhost()
    {
        if (intro_done)
        {
            audio.clip = normalGhostClip;
            audio.loop = true;
            audio.Play();
        }
    }

    public void playScaredGhost()
    {
        if (intro_done)
        {
            audio.clip = scaredGhostClip;
            audio.loop = true;
            audio.Play();
        }
    }

    public void playDeadGhost()
    {
        if (intro_done)
        {
            audio.clip = deadGhostClip;
            audio.loop = true;
            audio.Play();
        }
    }

    public void playDie()
    {
        if (intro_done)
        {
            audio.clip = dieClip;
            audio.loop = false;
            audio.Play();
        }
    }
}
