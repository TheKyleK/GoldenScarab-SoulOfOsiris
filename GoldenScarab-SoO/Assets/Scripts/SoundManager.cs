using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    Chime,
    Ding,
    PlayerStepStone,
    PlayerStepSand,
    MonsterStepStone,
    MonsterStepSand,
    MonsterGrowl,
    DoorOpen,
    DoorClose
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
        audioMap.Add(Sound.PlayerStepStone, Resources.Load<AudioClip>("Sounds/Player/PlayerStepStone"));
        audioMap.Add(Sound.PlayerStepSand, Resources.Load<AudioClip>("Sounds/Player/PlayerStepSand"));

        audioMap.Add(Sound.MonsterStepStone, Resources.Load<AudioClip>("Sounds/Monster/MonsterStepStone"));
        audioMap.Add(Sound.MonsterStepSand, Resources.Load<AudioClip>("Sounds/Monster/MonsterStepSand"));

        audioMap.Add(Sound.MonsterGrowl, Resources.Load<AudioClip>("Sounds/Monster/MonsterGrowl4"));

        audioMap.Add(Sound.DoorOpen, Resources.Load<AudioClip>("Sounds/Environment/DoorOpen"));
        audioMap.Add(Sound.DoorClose, Resources.Load<AudioClip>("Sounds/Environment/DoorClose"));
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
