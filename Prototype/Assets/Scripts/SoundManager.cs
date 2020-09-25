using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager current;

    public Dictionary<string, AudioClip> audioMap = new Dictionary<string, AudioClip>();

    public AudioClip placeholder;

    private void Awake()
    {
        current = this;
        
        //placeholder = Resources.Load<AudioClip>("Sounds/Chime");

        audioMap.Add("Chime", Resources.Load<AudioClip>("Sounds/Chime"));
      
        audioMap.Add("Ding", Resources.Load<AudioClip>("Sounds/Ding"));

        AudioSource.PlayClipAtPoint(clips[soundName], position);


    }

    public void PlaySound(AudioSource source, string soundName)
    {
        source.clip = audioMap[soundName];
        source.Play();



    }

}
