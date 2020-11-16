using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public static Setting current;
    [Range(0, 1)]
    public float volume;
    public float mouseSensitivity;
    private void Awake()
    {
        if (!current)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (current != this)
        {
            Debug.LogError("Destroying secondary instance.");
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GlobalManager.
            DestroyImmediate(gameObject);
            return;
        }
    }
}
