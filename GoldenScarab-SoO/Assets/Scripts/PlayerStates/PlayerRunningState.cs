using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : PlayerState
{
    public override PlayerState HandleInput()
    {
        if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
        {
            time = 0;
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


    public override void UpdateHeadBobbing(Camera camera, CharacterRB rb, float ampitude, float frequency, Vector3 originalPos)
    {
        float speedMultiplier = Util.Map(rb.GetVelocity().magnitude, 0, rb.GetMaxSpeed(), 0, 1);
        //Debug.Log(speedMultiplier);
        time += Time.deltaTime * speedMultiplier;
        float distance = -ampitude * Mathf.Sin(2 * Mathf.PI * frequency * time);
        if (Mathf.Sign(previousMove) != Mathf.Sign(distance) && distance > 0)
        {
            SoundManager.current.PlaySound(Sound.FootStep, camera.transform.position);
        }
        previousMove = distance;
        camera.transform.localPosition = originalPos +  Vector3.up * distance;
    }

}
