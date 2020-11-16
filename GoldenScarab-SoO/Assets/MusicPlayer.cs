using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource source;
    public float volumeMultiplier;

    private void Start()
    {
        source.volume = volumeMultiplier * SoundManager.current.volume;
        source.Play();
    }
    private void Update()
    {
        source.volume = volumeMultiplier * SoundManager.current.volume;
    }

}
