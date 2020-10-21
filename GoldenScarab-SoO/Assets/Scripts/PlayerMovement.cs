using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    CharacterRB m_rb;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<CharacterRB>();
    }

    // Update is called once per frame
    void Update() 
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * z + transform.right * x;
        Vector3 direction = movement.normalized;
        Vector3 force = direction * speed;

        m_rb.acceleration += force;
    }


}
