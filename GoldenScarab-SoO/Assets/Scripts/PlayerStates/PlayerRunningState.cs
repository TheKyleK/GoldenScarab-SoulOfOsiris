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

    public override void UpdateMovement(GameObject player, CharacterRB rb, float moveStrength)
    {
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

        rb.acceleration += moveForce;
    }

    public override void UpdateAnimation(Animator animator)
    {
        for (int i = 0; i < animator.parameters.Length; i++)
        {
            string param = animator.parameters[i].name;
            animator.SetBool(param, "Running".Equals(param));
        }
    }
}
