using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicLightSwitcher : MonoBehaviour
{
    public float radius;
    public LayerMask mask;
    public float offset;
    public float angle;

    public float radiusClose;

    // Start is called before the first frame update
    void Start()
    {
        EnableLights();
    }

    // Update is called once per frame
    void Update()
    {
        EnableLights();
    }

    void EnableLights()
    {
        Vector3 center = transform.position + Camera.main.transform.forward * offset;
        Transform[] targets = Physics.OverlapSphere(center, radius, mask).Select(x => x.transform).ToArray();

        for (int i = 0; i < targets.Length; i++)
        {
            Transform target = targets[i];

            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                target.gameObject.GetComponent<LightController>().Enable();
            }
        }

        Collider[] closeTargets = Physics.OverlapSphere(center, radiusClose, mask);

        for (int i = 0; i < closeTargets.Length; i++)
        {
            closeTargets[i].gameObject.GetComponent<LightController>().Enable();
        }
    }
}
