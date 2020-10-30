using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerState
{
    public override PlayerState HandleInput()
    {
        if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        {
            return new PlayerIdleState();
        }
        return null;
    }

    public override void UpdateRBDir(GameObject player, CharacterRB rb, float moveStrength)
    {
        //Vector3 moveDir = new Vector3();
        float x = 0;
        float z = 0;

        if (Input.GetButton("Horizontal"))
        {
            x = Input.GetAxis("Horizontal");
        }

        if (Input.GetButton("Vertical"))
        {
            z = Input.GetAxis("Vertical");
        }

        Vector3 move = player.transform.forward * z + player.transform.right * x;
        Vector3 moveForce = move.normalized * moveStrength;
        rb.force = moveForce;
    }

    public override void UpdateAnimation(Animator animator)
    {
        for (int i = 0; i < animator.parameters.Length; i++)
        {
            string param = animator.parameters[i].name;
            animator.SetBool(param, "Running".Equals(param));
        }
    }


    public override void UpdateHeadBobbing(Camera camera, CharacterRB rb, float ampitude, float period, Vector3 originalPos)
    {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = ampitude * Mathf.Sin(theta);
        offset += distance;
        //Debug.Log(offset);
        camera.transform.localPosition = originalPos + Vector3.up * offset;
    }

    //public override void UpdateHeadBobbing(Camera camera)
    //{

    //}

}
