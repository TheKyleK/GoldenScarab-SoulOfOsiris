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
    public float castDistanceY;
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
        Vector3 p1 = transform.position + Vector3.up * radius + center;
        Vector3 p2 = transform.position + Vector3.up * (height - radius) + center;

        RaycastHit hit;

        Vector3 moveUpward = Vector3.Scale(moveVec, Vector3.up);
        if (Physics.CapsuleCast(p1, p2, radius * radiusRatio, moveUpward.normalized, out hit, castDistanceY, groundMask))
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
