using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    Chime,
    Ding
}

public class SoundManager : MonoBehaviour
{
   
    // Start is called before the first frame update
    public static SoundManager current;

    public Dictionary<Sound, AudioClip> audioMap = new Dictionary<Sound, AudioClip>();

    private void Awake()
    {
        current = this;
        
        audioMap.Add(Sound.Chime, Resources.Load<AudioClip>("Sounds/Chime"));
      
        audioMap.Add(Sound.Ding, Resources.Load<AudioClip>("Sounds/Ding"));
        

    }

    public void PlaySound(Sound sound, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioMap[sound], position);
    }

}
