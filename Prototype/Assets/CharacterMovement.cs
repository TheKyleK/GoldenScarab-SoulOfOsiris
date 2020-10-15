using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterRB rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<CharacterRB>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 moveVec)
    {
        if (rb.isGrounded)
        {
            moveVec.y = 0;
        }
        transform.position += moveVec;
    }
}
