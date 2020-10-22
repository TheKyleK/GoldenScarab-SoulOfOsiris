using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Vector3 offset;
    public float radius;
    public float height;
    public LayerMask obstacleMask;
    public LayerMask groundMask;
    public float radiusRatio;
    public float castDistanceY;
    CharacterRB m_rb;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<CharacterRB>();
    }

    private bool IsWalkable(Vector3 moveDir, float amount)
	{
        RaycastHit hit;
        Vector3 p1 = transform.position + Vector3.up * radius + offset;
        Vector3 p2 = transform.position + Vector3.up * (height - radius) + offset;
        return !Physics.CapsuleCast(p1, p2, radius * radiusRatio, moveDir, out hit, amount, obstacleMask);
    }

	public void Move(Vector3 moveVec)
	{
        Vector3 moveForward = Vector3.Scale(moveVec, Vector3.forward);
        if (!IsWalkable(moveForward.normalized, moveForward.magnitude))
        {
            moveVec.z = 0;
        }
        Vector3 moveRight = Vector3.Scale(moveVec, Vector3.right);
        if (!IsWalkable(moveRight.normalized, moveRight.magnitude))
        {
            moveVec.x = 0;
        }

        transform.position += moveVec;
    }

    void OnDrawGizmosSelected()
    {
        Vector3 p1 = transform.position + Vector3.up * radius + offset;
        Vector3 p2 = transform.position + Vector3.up * (height - radius) + offset;
        Gizmos.DrawWireSphere(p1, radius * radiusRatio);
        Gizmos.DrawWireSphere(p2, radius * radiusRatio);
        //Gizmos.DrawRay
    }
}
