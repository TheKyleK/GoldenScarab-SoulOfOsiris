using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    public float stopRange;
    public LayerMask playerMask;
    public float rotateSpeed;

    public float viewRange;
    public Transform eyeTransform;
    public float viewAngle;
    public LayerMask obstacleMask;
    public float steeringForce;
    public float seekThreshold;

    public float lookAtPlayerRange;

    BTNode root;
    CharacterRB rb;
    Animator animator;
    NavMeshAgent agent;
    Blackboard blackboard;
    // Start is called before the first frame update
    void Start()
    {
        blackboard = new Blackboard();
        rb = GetComponent<CharacterRB>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        SelectorNode monsterBehaviour = new SelectorNode();

        // Stop Sequence
        {
            SequenceNode sequence = new SequenceNode();
            GetTargetsInRange inRnage = new GetTargetsInRange(playerMask, stopRange);
            GetClosest getClosest = new GetClosest(BlackboardKey.Input);
            Stop stop = new Stop(rb);
            RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
            UpdateAnimation updateAnimation = new UpdateAnimation(animator, "Idle");
            sequence.children.Add(inRnage);
            sequence.children.Add(getClosest);
            sequence.children.Add(stop);
            sequence.children.Add(rotateTowardsTarget);
            sequence.children.Add(updateAnimation);
            monsterBehaviour.children.Add(sequence);
        }

        // Seek Sequence
        {
            SequenceNode sequence = new SequenceNode();
            GetTargetsInRange inRange = new GetTargetsInRange(playerMask, viewRange);
            GetTargetsInLineOfSight inLineOfSignt = new GetTargetsInLineOfSight(eyeTransform, viewAngle, obstacleMask);
            StoreOutputDecorator storeClosestTarget = new StoreOutputDecorator(BlackboardKey.Storage);
            {
                GetClosest getClosest = new GetClosest(BlackboardKey.Input);
                storeClosestTarget.child = getClosest;
            }
            GetNextWaypoint getNextWayPoint = new GetNextWaypoint(agent, BlackboardKey.Input, seekThreshold);
            SeekTarget seekTarget = new SeekTarget(rb, BlackboardKey.Input, steeringForce);
            RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
            UpdateAnimation updateAnimation = new UpdateAnimation(animator, "Running");
            sequence.children.Add(inRange);
            sequence.children.Add(inLineOfSignt);
            sequence.children.Add(storeClosestTarget);
            sequence.children.Add(getNextWayPoint);
            sequence.children.Add(seekTarget);
            sequence.children.Add(rotateTowardsTarget);
            sequence.children.Add(updateAnimation);
            monsterBehaviour.children.Add(sequence);
        }

        // stop last saw position
        {
            DeleteMemoryDecorator removeStorageDecorator = new DeleteMemoryDecorator(BlackboardKey.Storage, BehaviourResult.Success);
            SequenceNode sequence = new SequenceNode();
            removeStorageDecorator.child = sequence;
            IsTargetInRange isTargetInRange = new IsTargetInRange(BlackboardKey.Storage, stopRange);
            Stop stop = new Stop(rb);
            UpdateAnimation updateAnimation = new UpdateAnimation(animator, "Idle");
            sequence.children.Add(isTargetInRange);
            sequence.children.Add(stop);
            sequence.children.Add(updateAnimation);
            monsterBehaviour.children.Add(removeStorageDecorator);
        }

        // Seek last saw position Sequence
        {
            SequenceNode sequence = new SequenceNode();
            GetNextWaypoint getNextWayPoint = new GetNextWaypoint(agent, BlackboardKey.Storage, seekThreshold);
            SeekTarget seekTarget = new SeekTarget(rb, BlackboardKey.Input, steeringForce);
            RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
            UpdateAnimation updateAnimation = new UpdateAnimation(animator, "Running");
            sequence.children.Add(getNextWayPoint);
            sequence.children.Add(seekTarget);
            sequence.children.Add(rotateTowardsTarget);
            sequence.children.Add(updateAnimation);
            monsterBehaviour.children.Add(sequence);
        }

        // look at player sequence
        {
            SequenceNode sequence = new SequenceNode();
            GetTargetsInRange inRnage = new GetTargetsInRange(playerMask, lookAtPlayerRange);
            GetClosest getClosest = new GetClosest(BlackboardKey.Input);
            Stop stop = new Stop(rb);
            RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
            UpdateAnimation updateAnimation = new UpdateAnimation(animator, "Idle");
            sequence.children.Add(inRnage);
            sequence.children.Add(getClosest);
            sequence.children.Add(stop);
            sequence.children.Add(rotateTowardsTarget);
            sequence.children.Add(updateAnimation);
            monsterBehaviour.children.Add(sequence);
        }

        // delete the input after the tree is finished
        DeleteMemoryDecorator removeOutputDecorator = new DeleteMemoryDecorator(BlackboardKey.Input, BehaviourResult.Success);
        removeOutputDecorator.child = monsterBehaviour;
        root = removeOutputDecorator;
    }

    // Update is called once per frame
    void Update()
    {
        root.Execute(gameObject, blackboard, Time.deltaTime);
    }
}
