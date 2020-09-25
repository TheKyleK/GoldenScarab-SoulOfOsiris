using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager current;

    public Dictionary<string, AudioClip> audioMap = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        current = this;
        
        audioMap.Add("Chime", Resources.Load<AudioClip>("Sounds/Chime"));
      
        audioMap.Add("Ding", Resources.Load<AudioClip>("Sounds/Ding"));
        

    }

    public void PlaySound(string soundName, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioMap[soundName], position);
    }

}
