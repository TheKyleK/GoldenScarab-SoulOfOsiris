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
    public float stepOffset;
    public int stepDetails;
    public float castAmount;
    CharacterRB m_rb;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<CharacterRB>();
    }

    private bool IsWalkable(Vector3 moveDir, float amount, LayerMask layerMask)
	{
        RaycastHit hit;
        Vector3 p1 = transform.position + Vector3.up * radius + offset;
        Vector3 p2 = transform.position + Vector3.up * (height - radius) + offset;
        return !Physics.CapsuleCast(p1, p2, radius * radiusRatio, moveDir, out hit, amount, layerMask);
    }

	public void Move(Vector3 moveVec)
	{
        Vector3 point = transform.position + offset + Vector3.up * radius * (1 - radiusRatio);
        int hitIndex = -1;
        int notHitIndex = -1;
        Vector3 moveVecHorizontal = new Vector3(moveVec.x, 0, moveVec.z);
        for (int i = 0; i < stepDetails; i++)
        {
            Vector3 currentPoint = point + Vector3.up * stepOffset / stepDetails * i;
            if (Physics.Raycast(currentPoint, moveVecHorizontal.normalized, radius + moveVecHorizontal.magnitude))
            {
                hitIndex = i;
                notHitIndex = -1;
            }
            else
            {
                if (hitIndex > -1 && notHitIndex == -1)
                {
                    notHitIndex = i;
                }
            }
            //Debug.DrawRay(currentPoint, moveVecHorizontal.normalized * (radius + moveVecHorizontal.magnitude), Color.green, 0.5f);
        }

        if (notHitIndex > -1)
        {
            //Debug.Log(notHitIndex);
            transform.position = new Vector3(transform.position.x, transform.position.y + stepOffset / stepDetails * notHitIndex, transform.position.z);
        }

        // gravity
        Vector3 moveUpward = Vector3.Scale(moveVec, Vector3.up);
        if (!IsWalkable(moveUpward.normalized, castDistanceY, groundMask))
        {
            moveVec.y = 0;
        }

        Vector3 moveForward = Vector3.Scale(moveVec, Vector3.forward);
        if (!IsWalkable(moveForward.normalized, castAmount, obstacleMask))
        {
            moveVec.z = 0;
        }
        Vector3 moveRight = Vector3.Scale(moveVec, Vector3.right);
        if (!IsWalkable(moveRight.normalized, castAmount, obstacleMask))
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
