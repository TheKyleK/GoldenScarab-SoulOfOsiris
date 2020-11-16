using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSensitivity : MonoBehaviour
{
    public static MouseSensitivity current;
    public float value;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        if (Setting.current)
        {
            value = Setting.current.mouseSensitivity;
        }
    }
}
