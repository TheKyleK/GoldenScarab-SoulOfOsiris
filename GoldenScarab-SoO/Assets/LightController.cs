using Boo.Lang.Environments;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light myLight;

    public void Start()
    {
        myLight.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    public void Enable()
    {
        myLight.gameObject.SetActive(true);
    }

    private void Update()
    {
        myLight.gameObject.SetActive(false);
    }
}
