using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake current;
    bool shaking = false;

    private void Awake()
    {
        current = this;
    }
    public void Shake(float magX, float magY, float decayRate, float minimum)
    {
        if (!shaking)
        {
            StartCoroutine(DoShake(magX, magY, decayRate, minimum));
        }
    }


    IEnumerator DoShake(float magX, float magY, float decayRate, float minimum)
    {
        float shake = 1.0f;
        shaking = true;
        while (shake > 0)
        {
            float shakeX = Random.Range(-magX, magX);
            float shakeY = Random.Range(-magY, magY);
            shakeX *= shake;
            shakeY *= shake;
            transform.localPosition = new Vector3(shakeX, shakeY);
            shake *= decayRate;
            if (shake < minimum)
            {
                transform.localPosition = new Vector3(0, 0);
                break;
            }
            yield return null;
        }
        shaking = false;
        yield return null;
    }
}
