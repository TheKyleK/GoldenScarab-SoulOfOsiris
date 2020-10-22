using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEventManager : MonoBehaviour
{
    private bool entered;

    [ColorUsage(true, true)]
    public Color color1;
    [ColorUsage(true, true)]
    public Color color2;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.onObjectTriggerEnter += OnObjectTriggerEnter;
        EventManager.current.onObjectTriggerExit += OnObjectTriggerExit;
        GetComponent<Renderer>().material.SetColor("_Color", color1);
    }
    void OnObjectTriggerEnter(GameObject obj)
    {
        if (obj == gameObject)
        {
            entered = true;
        }
    }

    void OnObjectTriggerExit(GameObject obj)
    {
        if (obj == gameObject)
        {
            entered = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (entered)
        {
            //GetComponent<Renderer>().material.shader = Shader.Find("HDRenderPipeline/Lit");
            GetComponent<Renderer>().material.SetColor("_Color", color2);
        }
        else
        {
            //GetComponent<Renderer>().material.shader = Shader.Find("HDRenderPipeline/Lit");
            GetComponent<Renderer>().material.SetColor("_Color", color1);

        }
    }
}
