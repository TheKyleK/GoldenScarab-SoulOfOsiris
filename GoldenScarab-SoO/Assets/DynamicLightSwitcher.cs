using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLightSwitcher : MonoBehaviour
{
    public float radius;
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, mask);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.gameObject.GetComponent<LightController>().Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, mask);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.gameObject.GetComponent<LightController>().Enable();
        }
    }
}
