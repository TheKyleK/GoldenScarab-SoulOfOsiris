using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterRB : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 acceleration;
    public float friction;

    public float speed;
    public CharacterController controller;

    //public bool updateRotation;
    void FixedUpdate()
    {
        ClampMoveSpeed();
        velocity += acceleration * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);
        ApplyFriction();
        //UpdateRotation();
        acceleration *= 0;
    }

    void ApplyFriction()
    {
        float decay = Mathf.Pow(1 - friction, Time.deltaTime);
        velocity.x *= decay;
        velocity.z *= decay;
    }

    void ClampMoveSpeed()
    {
        Vector3 veloctiyXZ = new Vector3(velocity.x, 0, velocity.z);
        if (veloctiyXZ.magnitude > speed)
        {
            veloctiyXZ = veloctiyXZ.normalized;
            veloctiyXZ *= speed;
            velocity.x = veloctiyXZ.x;
            velocity.z = veloctiyXZ.z;
        }
    }


    //void UpdateRotation()
    //{
    //    if (updateRotation)
    //    {
    //        if (velocity.magnitude > 0)
    //        {
    //            float heading = Mathf.Atan2(velocity.x, velocity.z);
    //            transform.localRotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);
    //        }
    //        else
    //        {
    //            // not stable
    //            MonsterMovement mm = GetComponent<MonsterMovement>();
    //            if (mm)
    //            {
    //                Vector3 playerPos = mm.player.transform.position;
    //                float heading = Mathf.Atan2(transform.position.x - playerPos.x, transform.position.z - playerPos.z);
    //                transform.localRotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg + 180, 0);
    //            }
    //        }
    //    }
    //}
}
