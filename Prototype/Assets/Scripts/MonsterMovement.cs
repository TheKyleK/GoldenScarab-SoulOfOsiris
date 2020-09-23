using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    public GameObject player;
    public float stoppingDistance;
    public float steeringForce;

    NavMeshPath path;
    CharacterRB rb;
    NavMeshAgent agent;

    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<CharacterRB>();
        path = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        SeekPath();
        Arrive();
    }

    void SeekPath()
    {
        if (agent.CalculatePath(player.transform.position, path))
        {
            for (int i = 0; i < path.corners.Length; i++)
            {
                Vector3 target = path.corners[i];
                if ((target - transform.position).magnitude > 1)
                {
                    Vector3 desireVelocity = (target - transform.position).normalized * rb.speed;
                    Vector3 force = (desireVelocity - rb.velocity) * steeringForce;

                    rb.acceleration += force;
                    break;
                }
            }
        }
    }

    void Arrive()
    {
        Vector3 agentXZ = transform.position;
        agentXZ.y = 0;
        Vector3 playerXZ = player.transform.position;
        playerXZ.y = 0;

        if ((agentXZ - playerXZ).magnitude < stoppingDistance)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.acceleration = new Vector3(0, rb.acceleration.y, 0);
        }
    }
}
