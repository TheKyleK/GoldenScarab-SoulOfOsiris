using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTeleportEventManager : MonoBehaviour
{
    public TriggerEventManager trigger;
    public GameObject monster;
    private MonsterBehaviour m_mb;
    private CharacterRB m_rb;
    private Animator m_animator;

    void Start()
    {
        m_mb = monster.GetComponent<MonsterBehaviour>();
        m_rb = monster.GetComponent<CharacterRB>();
        m_animator = monster.GetComponent<Animator>();
        EventManager.current.onTriggerActivated += OnTriggerActivated;
    }

    void OnTriggerActivated(GameObject obj)
    {
        if (trigger.triggered)
        {
            //float dotprod = Vector3.Dot(m_rb.GetVelocity(), new Vector3(0, 0, 1));
            //if (dotprod > 0)
            //{
            SequenceNode sequenceBefore = new SequenceNode();
            TreeNode removeStorageDecorator = new DeleteMemoryDecorator(sequenceBefore, BlackboardKey.Storage, BehaviourResult.Failure);
            SequenceNode sequenceAfter = new SequenceNode();
            Stop stop = new Stop(m_rb);         
            UpdateAnimation updateAnimation = new UpdateAnimation(m_animator, "Idle");
            SetPosition setPosition = new SetPosition(transform.position);
            sequenceAfter.Add(stop);
            sequenceAfter.Add(updateAnimation);
            sequenceAfter.Add(setPosition);
            m_mb.EnqueueBefore(removeStorageDecorator);
            m_mb.EnqueueAfter(sequenceAfter);

            //}
            //m_mb.Execute(removeStorageDecorator);
            //monster.SetActive(false);
            //monster.transform.position = transform.position;
            //monster.SetActive(true);
        }
    }
}
