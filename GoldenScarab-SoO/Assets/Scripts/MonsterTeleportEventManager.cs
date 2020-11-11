using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTeleportEventManager : MonoBehaviour
{
    public List<TriggerEventManager> triggers;
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
        EventManager.current.onTriggerDeactivated += OnTriggerDeactivated;
    }

    void OnTriggerActivated(GameObject obj)
    {
        TeleportPlayer(obj);
    }

    void OnTriggerDeactivated(GameObject obj)
    {
        TeleportPlayer(obj);
    }

    void TeleportPlayer(GameObject obj)
    {
        foreach(TriggerEventManager trigger in triggers)
        {
            if (trigger.gameObject == obj && Vector3.Distance(monster.transform.position, transform.position) > 1.0f)
            {
                //SequenceNode sequenceBefore = new SequenceNode();
                SequenceNode sequenceAfter = new SequenceNode();
                Stop stop = new Stop(m_rb);
                UpdateAnimation updateAnimation = new UpdateAnimation(m_animator, "Idle");
                SetPosition setPosition = new SetPosition(transform.position);
                TreeNode removeStorageDecorator = new DeleteLastKnowPositionDecorator(sequenceAfter, BehaviourResult.Success);
                sequenceAfter.Add(stop);
                sequenceAfter.Add(updateAnimation);
                sequenceAfter.Add(setPosition);
                //m_mb.EnqueueBefore(removeStorageDecorator);
                m_mb.EnqueueAfter(removeStorageDecorator);
                break;
            }
        }
    }
}
