using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;


public class CharacterState : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walking,
        Running
    }

    CharacterRB rb;
    Animator animator;
    [SerializeField]
    State state = State.Idle;
    //public float runThreshold;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<CharacterRB>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        UpdateAnimation();
    }

    void UpdateState()
    {
        Vector3 velocityXZ = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        float mag = velocityXZ.magnitude;
        if (mag > 0)
        {
            state = State.Running;
        }
        else
        {
            state = State.Idle;
        }

    }

    void UpdateAnimation()
    {
        for (int i = 0; i < animator.parameters.Length; i++)
        {
            string param = animator.parameters[i].name;
            animator.SetBool(param, state.ToString().Equals(param));
        }
    }
}
