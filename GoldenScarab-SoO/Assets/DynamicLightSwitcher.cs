using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLightSwitcher : MonoBehaviour
{
    public float radius;
    public LayerMask mask;
    public float offset;

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
        Vector3 center = transform.position + Camera.main.transform.forward * offset;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, mask);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.gameObject.GetComponent<LightController>().Enable();
        }
    }
}
