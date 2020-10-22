using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterNavigation : MonoBehaviour
{
    //Private varibles does have "m_"
    private CharacterRB m_rb;
    private CharacterController m_controller;
    private NavMeshAgent m_agent;
    public GameObject player;
    public float threshold;
    public float steeringForce;
    

	private void Start()
	{
		m_agent = GetComponent<NavMeshAgent>();
        m_controller = GetComponent<CharacterController>();
        m_rb = GetComponent<CharacterRB>();
	}

	private void Update()
	{
        //agent.SetDestination(player.transform.position);

        NavMeshPath path = new NavMeshPath();
        Vector3 targetPos = player.transform.position;
        if (m_agent.CalculatePath(targetPos, path))
        {
            for (int i = 0; i < path.corners.Length; i++)
            {
                Vector3 target = path.corners[i];
                if ((target - m_agent.transform.position).magnitude > threshold)
                {
                    Vector3 desireVelocity = (target - m_agent.transform.position).normalized * m_rb.maxSpeed;
                    Vector3 force = (desireVelocity - m_rb.velocity) * steeringForce;
                    m_rb.acceleration += force;
                    break;
                }
            }
        }
    }

}
