using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise:MonoBehaviour
{


    public float amplitude;
    public float frequency;

    Vector3 noise;
    Vector3 noiseOffset;


    public void PerlinNoiseCreator()
    {

        float _rand = 32f;

        noiseOffset.x = Random.Range(0f, _rand);
        noiseOffset.y = Random.Range(0f, _rand);
        noiseOffset.z = Random.Range(0f, _rand);


    }

    public void UpdateNoise()
    {
        float _scrollOffset = Time.deltaTime * frequency;

        noiseOffset.x += _scrollOffset;
        noiseOffset.y += _scrollOffset;
        noiseOffset.z += _scrollOffset;

        noise.x = Mathf.PerlinNoise(noiseOffset.x, 0f);
        noise.y = Mathf.PerlinNoise(noiseOffset.x, 1f);
        noise.z = Mathf.PerlinNoise(noiseOffset.x, 2f);

        noise -= Vector3.one * 0.5f;
        noise *= amplitude;
    }

    private void LateUpdate()
    {
        UpdateNoise();

        Vector3 rotationOffset = Vector3.zero;

        rotationOffset.x += noise.x;
        rotationOffset.y += noise.y;

        transform.localEulerAngles += rotationOffset;

    }



}
