using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    Chime,
    Ding,
    FootStep,
    FootStepWood
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager current;
    [Range(0, 1)]
    public float volume;

    public Dictionary<Sound, AudioClip> audioMap = new Dictionary<Sound, AudioClip>();
    private void Awake()
    {
        current = this;

        audioMap.Add(Sound.Chime, Resources.Load<AudioClip>("Sounds/Chime"));
        audioMap.Add(Sound.Ding, Resources.Load<AudioClip>("Sounds/Ding"));
        audioMap.Add(Sound.FootStep, Resources.Load<AudioClip>("Sounds/FootStep"));
        audioMap.Add(Sound.FootStepWood, Resources.Load<AudioClip>("Sounds/FootStepWood"));

    }

    /// <summary>
    /// Play sound at position using global volume
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="position"></param>
    public void PlaySound(Sound sound, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioMap[sound], position, volume);
    }

    
    /// <summary>
    /// Play sound at position with a percentage of volume
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="position"></param>
    /// <param name="volume"></param>
    public void PlaySound(Sound sound, Vector3 position, float volume)
    {
        AudioSource.PlayClipAtPoint(audioMap[sound], position, volume * this.volume);
    }
}
