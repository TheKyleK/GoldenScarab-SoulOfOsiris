using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public override PlayerState HandleInput()
    {
        return null;
    }

    public override void UpdateAnimation(Animator animator)
    {
        
    }

    public override void UpdateHeadBobbing(Camera camera, CharacterRB rb, float ampitude, float frequency, Vector3 originalPos, FootSteps footSteps)
    {
        
    }

    public override void UpdateRBDir(GameObject player, CharacterRB rb, float moveStrength)
    {
        rb.SetVelocity(Vector3.zero);
    }
}
