using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeController : MonoBehaviour
{
    public float volume;
    public AudioSource audioSource;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = Setting.current.volume * volume;
    }
}
