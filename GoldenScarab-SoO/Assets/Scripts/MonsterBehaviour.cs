using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    public float stopRange;


    public LayerMask playerMask;
    public float viewRange;
    public Transform eyeTransform;
    public float viewAngle;
    public LayerMask obstacleMask;
    public float seekThreshold;
    public float steeringForce;
    public float rotateSpeed;

    private NavMeshAgent m_agent;
    private CharacterRB m_rb;
    private Animator m_animator;
    private TreeNode m_root;
    private Blackboard m_blackboard;
    

    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_rb = GetComponent<CharacterRB>();
        m_animator = GetComponent<Animator>();

        m_root = ConstructBehaviourTree();
        m_blackboard = new Blackboard();
    }

    // Update is called once per frame
    void Update()
    {
        m_root.Execute(gameObject, m_blackboard, Time.deltaTime);
    }

    TreeNode ConstructBehaviourTree()
    {
        SelectorNode monsterBehaviour = new SelectorNode();

        // Stop Sequence
        TreeNode stopSequence = CreateStopSequence();

        // Seek Sequence
        TreeNode seekPlayerSequence = CreateSeekPlayerSequence();

        monsterBehaviour.Add(stopSequence);
        monsterBehaviour.Add(seekPlayerSequence);

        return monsterBehaviour;
    }

    private TreeNode CreateStopSequence()
    {
        SequenceNode sequence = new SequenceNode();
        GetTargetsInRange inRnage = new GetTargetsInRange(playerMask, stopRange);
        GetClosest getClosest = new GetClosest(BlackboardKey.Input);
        Stop stop = new Stop(m_rb);
        RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
        UpdateAnimation updateAnimation = new UpdateAnimation(m_animator, "Idle");
        sequence.Add(inRnage);
        sequence.Add(getClosest);
        sequence.Add(stop);
        sequence.Add(rotateTowardsTarget);
        sequence.Add(updateAnimation);

        //if (debug)
        //{
        //    return AttachDebugLog(sequence, "Stop", BehaviourResult.Success);
        //}
        return sequence;
    }

    private TreeNode CreateSeekPlayerSequence()
    {
        SequenceNode sequence = new SequenceNode();
        GetTargetsInRange inRange = new GetTargetsInRange(playerMask, viewRange);
        GetTargetsInLineOfSight inLineOfSignt = new GetTargetsInLineOfSight(eyeTransform, viewAngle, obstacleMask);
        GetClosest getClosest = new GetClosest(BlackboardKey.Input);
        StoreOutputDecorator storeClosestTarget = new StoreOutputDecorator(getClosest, BlackboardKey.Storage);
        GetNextWaypoint getNextWayPoint = new GetNextWaypoint(m_agent, BlackboardKey.Input, seekThreshold);
        SeekTarget seekTarget = new SeekTarget(m_rb, BlackboardKey.Input, steeringForce);
        RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
        UpdateAnimation updateAnimation = new UpdateAnimation(m_animator, "Running");
        sequence.Add(inRange);
        sequence.Add(inLineOfSignt);
        sequence.Add(storeClosestTarget);
        sequence.Add(getNextWayPoint);
        sequence.Add(seekTarget);
        sequence.Add(rotateTowardsTarget);
        sequence.Add(updateAnimation);

        //if (debug)
        //{
        //    return AttachDebugLog(sequence, "Seek Player", BehaviourResult.Success);
        //}
        return sequence;
    }


}
