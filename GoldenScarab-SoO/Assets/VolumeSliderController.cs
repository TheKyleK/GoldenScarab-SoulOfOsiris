using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class VolumeSliderController : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public Slider slider;

    private void Start()
    {   
        UpdateValue(Setting.current.volume);
    }
    public void OnSliderChanged(float value)
    {
        UpdateValue(value);
    }

    private void UpdateValue(float value)
    {
        Setting.current.volume = value;
        if (SoundManager.current)
        {
            SoundManager.current.volume = value;
        }
        tmp.text = (value * 100).ToString("F0") + "%";
        slider.value = value;
    }
}
