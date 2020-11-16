using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MouseSensitivitySliderController : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public Slider slider;

    private void Start()
    {
        UpdateValue(Setting.current.mouseSensitivity);
    }
    public void OnSliderChanged(float value)
    {
        UpdateValue(value);
    }

    private void UpdateValue(float value)
    {
        Setting.current.mouseSensitivity = value;
        if (MouseSensitivity.current)
        {
            MouseSensitivity.current.value = value;
        }
        tmp.text = (value).ToString("F0");
        slider.value = value;
    }
}
