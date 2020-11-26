using Boo.Lang.Environments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light myLight;
    public AudioSource audio;
    public GameObject particles;

    public void Start()
    {
        Enable();
        //myLight.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    public void Enable()
    {
        myLight.gameObject.SetActive(true);
        particles.SetActive(true);
    }

    private void Update()
    {
        myLight.gameObject.SetActive(false);
        particles.SetActive(false);
    }
}
