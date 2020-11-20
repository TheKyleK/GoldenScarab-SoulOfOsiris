using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light myLight;
    public float flickeringIntensity;
    public float flickeringRate;
    private float initIntensity;
    float offset;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        initIntensity = myLight.intensity;
        offset = Random.Range(0, 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        myLight.intensity = initIntensity - (Mathf.PerlinNoise(offset + Time.time * flickeringRate, 0) * flickeringIntensity);
    }
}
