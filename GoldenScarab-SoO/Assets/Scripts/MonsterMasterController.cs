using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMasterController : CharacterMasterController
{
    [Header("Stop")]
    public float stopRange;
    [Header("Seek")]
    public float viewRange;
    public float viewAngle;
    [Header("Look At")]
    public float lookAtPlayerRange;
   
}
