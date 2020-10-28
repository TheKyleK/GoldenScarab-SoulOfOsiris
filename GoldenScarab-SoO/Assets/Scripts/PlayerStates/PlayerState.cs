using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public abstract PlayerState HandleInput();
    public abstract void UpdateRBDir(GameObject player, CharacterRB rb, float moveStrength);
    public abstract void UpdateAnimation(Animator animator);
}
