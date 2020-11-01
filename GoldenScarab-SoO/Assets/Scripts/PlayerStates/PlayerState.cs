using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected float offset = 0;
    protected float time = 0;
    protected float previousMove = 0;
    public abstract PlayerState HandleInput();
    public abstract void UpdateRBDir(GameObject player, CharacterRB rb, float moveStrength);
    public abstract void UpdateAnimation(Animator animator);

    public abstract void UpdateHeadBobbing(Camera camera, CharacterRB rb, float ampitude, float frequency, Vector3 originalPos);
}
