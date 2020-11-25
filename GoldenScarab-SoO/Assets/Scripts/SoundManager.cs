using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    PickUp,
    PutDown,
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
        if (Setting.current)
        {
            volume = Setting.current.volume;
        }

        audioMap.Add(Sound.PickUp, Resources.Load<AudioClip>("Updated/Environment/pickup"));
        Debug.Log(audioMap[Sound.PickUp]);
        audioMap.Add(Sound.PutDown, Resources.Load<AudioClip>("Updated/Environment/PLACING"));
        audioMap.Add(Sound.PlayerStepStone, Resources.Load<AudioClip>("Sounds/Player/PlayerStepStone"));
        audioMap.Add(Sound.PlayerStepSand, Resources.Load<AudioClip>("Sounds/Player/PlayerStepSand"));

        audioMap.Add(Sound.MonsterStepStone, Resources.Load<AudioClip>("Sounds/Monster/MonsterStepStone"));
        audioMap.Add(Sound.MonsterStepSand, Resources.Load<AudioClip>("Sounds/Monster/MonsterStepSand"));

        audioMap.Add(Sound.MonsterGrowl, Resources.Load<AudioClip>("Sounds/Monster/MonsterGrowl4"));

        audioMap.Add(Sound.DoorOpen, Resources.Load<AudioClip>("Sounds/Environment/DoorOpen"));
        audioMap.Add(Sound.DoorClose, Resources.Load<AudioClip>("Sounds/Environment/DoorClose"));
    }

    AudioSource PlayClipAt(AudioClip clip, Vector3 pos, float volume)
    {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        // set other aSource properties here, if desired
        aSource.volume = volume;
        aSource.dopplerLevel = 0;
        aSource.Play(); // start the sound
        Destroy(tempGO, clip.length); // destroy object after clip duration
        return aSource; // return the AudioSource reference
    }


    /// <summary>
    /// Play sound at position using global volume
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="position"></param>
    public void PlaySound(Sound sound, Vector3 position)
    {
        PlayClipAt(audioMap[sound], position, volume);
    }

    
    /// <summary>
    /// Play sound at position with a percentage of volume
    /// </summary>
    /// <param name="sound"></param>
    /// <param name="position"></param>
    /// <param name="multiplier"></param>
    public void PlaySound(Sound sound, Vector3 position, float multiplier)
    {
        PlayClipAt(audioMap[sound], position, multiplier * volume);
    }
}
