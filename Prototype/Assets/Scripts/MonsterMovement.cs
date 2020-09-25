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

    FieldOfView fov;
    Vector3 lastSaw;

    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<CharacterRB>();
        path = new NavMeshPath();
        agent = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
        lastSaw = transform.position;
    }
    void Update()
    {
        if (fov.visibleTargets.Count > 0)
        {
            SeekPath(fov.visibleTargets[0]);
            lastSaw = fov.visibleTargets[0];
            //lastSaw = fov.visibleTargets
        }
        else
        {
            SeekPath(lastSaw);
        }
        Arrive();
    }

    void SeekPath(Vector3 targetPos)
    {
        if (agent.CalculatePath(targetPos, path))
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
        Vector3 playerXZ = lastSaw;
        playerXZ.y = 0;

        if ((agentXZ - playerXZ).magnitude < stoppingDistance)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.acceleration = new Vector3(0, rb.acceleration.y, 0);
        }
    }
}
