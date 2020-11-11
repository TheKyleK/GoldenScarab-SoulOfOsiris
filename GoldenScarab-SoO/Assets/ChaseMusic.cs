using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseMusic : MonoBehaviour
{
    public AudioSource source;
    public float volume;
    bool playing = false;

    private void Start()
    {
        source.Stop();
    }

    public void Play(float fadeinTime)
    {
        if (!playing)
        {
            StartCoroutine(PlayMusic(fadeinTime));
        }
    }

    IEnumerator PlayMusic(float fadeinTime)
    {
        float time = 0;
        playing = true;
        source.Play();
        while(time < fadeinTime)
        {
            float currentVolume = time / fadeinTime;
            time += Time.deltaTime;
            source.volume = volume * SoundManager.current.volume * currentVolume;
            yield return null;
        }
        yield return null;
    }

    public void Stop(float fadeoutTime)
    {
        if (playing)
        {
            StartCoroutine(StopMusic(fadeoutTime));
        }
    }

    IEnumerator StopMusic(float fadeoutTime)
    {
        float time = 0;
        while (time < fadeoutTime)
        {
            float currentVolume = 1 - time / fadeoutTime;
            source.volume = volume * SoundManager.current.volume * currentVolume;
            time += Time.deltaTime;
            yield return null;
        }
        playing = false;
        source.Stop();
        yield return null;
    }
}
