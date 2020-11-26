using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPlayerHeight : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }
}
