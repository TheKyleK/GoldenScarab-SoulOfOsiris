using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Vector3 center;
    public float radius;
    public float height;
    public LayerMask obstacleMask;
    public LayerMask groundMask;
    public float radiusRatio;
    CharacterRB rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<CharacterRB>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 p1 = transform.position + new Vector3(0, radius, 0);
        //Vector3 p2 = transform.position + new Vector3(0, radius, 0) + new Vector3(0, height - radius * 2, 0);
        //Gizmos.DrawWireSphere(p1, radius);
        //Gizmos.DrawWireSphere(p2, radius);
        //Vector3 p1 = transform.position + center + Vector3.up * -height * 0.5F;
        //Vector3 p2 = p1 + Vector3.up * height;
    }

    public void Move(Vector3 moveVec)
    {
        Vector3 p1 = transform.position + Vector3.up * radius + center;
        Vector3 p2 = transform.position + Vector3.up * (height - radius) + center;

        //float dist = moveVec.magnitude;
        RaycastHit hit;
        //if (rb.isGrounded)
        //{
        //    moveVec.y = 0;

        //}

        //hits = Physics.CapsuleCastAll(p1, p2, radius, Vector3.down, 0, groundMask);
        //foreach(RaycastHit hit in hits)
        //{
        //    moveVec.y = 0;
        //}

        //hits = Physics.CapsuleCastAll(p1, p2, radius, Vector3.Scale(moveVec, Vector3.forward).normalized, 0, obstacleMask);
        //foreach (RaycastHit hit in hits)
        //{
        //    moveVec.z = 0;
        //}

        //hits = Physics.CapsuleCastAll(p1, p2, radius, Vector3.Scale(moveVec, Vector3.right).normalized, 0, obstacleMask);
        //foreach (RaycastHit hit in hits)
        //{
        //    moveVec.x = 0;
        //}

        Vector3 moveUpward = Vector3.Scale(moveVec, Vector3.up);
        if (Physics.CapsuleCast(p1, p2, radius * radiusRatio, moveUpward.normalized, out hit, moveUpward.magnitude, groundMask))
        {
            moveVec.y = 0;
        }

        Vector3 moveForward = Vector3.Scale(moveVec, Vector3.forward);
        if (Physics.CapsuleCast(p1, p2, radius * radiusRatio, moveForward.normalized, out hit, moveForward.magnitude, obstacleMask))
        {
            moveVec.z = 0;
        }

        Vector3 moveRight = Vector3.Scale(moveVec, Vector3.right);
        if (Physics.CapsuleCast(p1, p2, radius * radiusRatio, moveRight.normalized, out hit, moveRight.magnitude, obstacleMask))
        {
            moveVec.x = 0;
        }

        transform.position += moveVec;

    }

    void OnDrawGizmosSelected()
    {
        Vector3 p1 = transform.position + Vector3.up * radius + center;
        Vector3 p2 = transform.position + Vector3.up * (height - radius) + center;
        Gizmos.DrawWireSphere(p1, radius * radiusRatio);
        Gizmos.DrawWireSphere(p2, radius * radiusRatio);
    }
}
