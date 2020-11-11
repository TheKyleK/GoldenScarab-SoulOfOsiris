using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override PlayerState HandleInput()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            time = 0;
            return new PlayerRunningState();
        }
        return null;
    }

    public override void UpdateRBDir(GameObject player, CharacterRB rb, float moveStrength)
    {
        rb.force *= 0;
        //rb.dirX = 0;
        //rb.dirZ = 0;
    }


    public override void UpdateAnimation(Animator animator)
    {
        for (int i = 0; i < animator.parameters.Length; i++)
        {
            string param = animator.parameters[i].name;
            animator.SetBool(param, "Idle".Equals(param));
        }
    }


    public override void UpdateHeadBobbing(Camera camera, CharacterRB rb, float ampitude, float frequency, Vector3 originalPos, FootSteps footSteps)
    {
        float current = Mathf.Lerp(camera.transform.localPosition.y, originalPos.y, 10 * Time.deltaTime);
        offset = current - originalPos.y;
        camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, current, camera.transform.localPosition.z);
    }


}
