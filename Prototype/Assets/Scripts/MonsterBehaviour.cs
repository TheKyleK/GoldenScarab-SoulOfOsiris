using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    [Header("Stop Sequence")]
    public LayerMask playerMask;
    public float stopRange;
    public float rotateSpeed;

    [Header("Seek Sequence")]
    public float viewRange;
    public Transform eyeTransform;
    public float viewAngle;
    public LayerMask obstacleMask;
    public float steeringForce;
    public float seekThreshold;

    [Header("Look At Player Sequence")]
    public float lookAtPlayerRange;

    [Header("Behaviour tree delay")]
    public float delay;

    [Header("Debug Draw")]
    public bool debug;
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDistanceThreshold;
    public MeshFilter viewMeshFilter;
    [HideInInspector]
    public BTNode root;

    Mesh viewMesh;
    CharacterRB rb;
    Animator animator;
    NavMeshAgent agent;
    public Blackboard blackboard;
    // Start is called before the first frame update
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        blackboard = new Blackboard();
        rb = GetComponent<CharacterRB>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        root = ConstructBehaviourTree();

        StartCoroutine("ExecuteTree");
    }

    IEnumerator ExecuteTree()
    {
        while (true)
        {
            root.Execute(gameObject, blackboard, delay);
            if (debug)
            {
                DrawFieldOfView();
            }
            yield return new WaitForSeconds(delay);
        }
    }


    BTNode ConstructBehaviourTree()
    {
        SelectorNode monsterBehaviour = new SelectorNode();

        // Stop Sequence
        BTNode stopSequence = CreateStopSequence();

        // Seek Sequence
        BTNode seekPlayerSequence = CreateSeekPlayerSequence();

        // stop last saw position
        BTNode stopLastSawPositionSequence = CreateStopLastSawPositionSequence();

        // Seek last saw position Sequence
        BTNode seekLastSawPositionSequence = CreateSeekLastSawPositionSequence();

        // look at player sequence
        BTNode lookAtPlayerSequence = CreateLookAtPlayerSequence();

        monsterBehaviour.Add(stopSequence);
        monsterBehaviour.Add(seekPlayerSequence);
        monsterBehaviour.Add(stopLastSawPositionSequence);
        monsterBehaviour.Add(seekLastSawPositionSequence);
        monsterBehaviour.Add(lookAtPlayerSequence);

        // delete the input after the tree is finished
        DeleteMemoryDecorator deleteMemoryDecorator = new DeleteMemoryDecorator(BlackboardKey.Input, BehaviourResult.Success);
        deleteMemoryDecorator.child = monsterBehaviour;
        return deleteMemoryDecorator;
    }

    public BTNode CreateStopSequence()
    {
        SequenceNode sequence = new SequenceNode();
        GetTargetsInRange inRnage = new GetTargetsInRange(playerMask, stopRange);
        GetClosest getClosest = new GetClosest(BlackboardKey.Input);
        Stop stop = new Stop(rb);
        RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
        UpdateAnimation updateAnimation = new UpdateAnimation(animator, "Idle");
        sequence.Add(inRnage);
        sequence.Add(getClosest);
        sequence.Add(stop);
        sequence.Add(rotateTowardsTarget);
        sequence.Add(updateAnimation);

        if (debug)
        {
            return AttachDebugLog(sequence, "Stop", BehaviourResult.Success);
        }
        return sequence;
    }

    public BTNode CreateSeekPlayerSequence()
    {
        SequenceNode sequence = new SequenceNode();
        GetTargetsInRange inRange = new GetTargetsInRange(playerMask, viewRange);
        GetTargetsInLineOfSight inLineOfSignt = new GetTargetsInLineOfSight(eyeTransform, viewAngle, obstacleMask);
        GetClosest getClosest = new GetClosest(BlackboardKey.Input);
        StoreOutputDecorator storeClosestTarget = new StoreOutputDecorator(BlackboardKey.Storage)
        {
            child = getClosest
        };
        GetNextWaypoint getNextWayPoint = new GetNextWaypoint(agent, BlackboardKey.Input, seekThreshold);
        SeekTarget seekTarget = new SeekTarget(rb, BlackboardKey.Input, steeringForce);
        RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
        UpdateAnimation updateAnimation = new UpdateAnimation(animator, "Running");
        sequence.Add(inRange);
        sequence.Add(inLineOfSignt);
        sequence.Add(storeClosestTarget);
        sequence.Add(getNextWayPoint);
        sequence.Add(seekTarget);
        sequence.Add(rotateTowardsTarget);
        sequence.Add(updateAnimation);

        if (debug)
        {
            return AttachDebugLog(sequence, "Seek Player", BehaviourResult.Success);
        }
        return sequence;
    }

    public BTNode CreateStopLastSawPositionSequence()
    {
        SequenceNode sequence = new SequenceNode();
        BTNode removeStorageDecorator = new DeleteMemoryDecorator(BlackboardKey.Storage, BehaviourResult.Success)
        {
            child = sequence
        };

        IsTargetInRange isTargetInRange = new IsTargetInRange(BlackboardKey.Storage, stopRange);
        Stop stop = new Stop(rb);
        UpdateAnimation updateAnimation = new UpdateAnimation(animator, "Idle");
        sequence.Add(isTargetInRange);
        sequence.Add(stop);
        sequence.Add(updateAnimation);

        if (debug)
        {
            return AttachDebugLog(removeStorageDecorator, "Stop At Last Saw Position", BehaviourResult.Success);
        }
        return removeStorageDecorator;
    }

    public BTNode CreateSeekLastSawPositionSequence()
    {
        SequenceNode sequence = new SequenceNode();
        GetNextWaypoint getNextWayPoint = new GetNextWaypoint(agent, BlackboardKey.Storage, seekThreshold);
        SeekTarget seekTarget = new SeekTarget(rb, BlackboardKey.Input, steeringForce);
        RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
        UpdateAnimation updateAnimation = new UpdateAnimation(animator, "Running");
        sequence.Add(getNextWayPoint);
        sequence.Add(seekTarget);
        sequence.Add(rotateTowardsTarget);
        sequence.Add(updateAnimation);
        if (debug)
        {
            return AttachDebugLog(sequence, "Seek Last Saw Position", BehaviourResult.Success);
        }
        return sequence;
    }

    public BTNode CreateLookAtPlayerSequence()
    {
        SequenceNode sequence = new SequenceNode();
        GetTargetsInRange inRnage = new GetTargetsInRange(playerMask, lookAtPlayerRange);
        GetClosest getClosest = new GetClosest(BlackboardKey.Input);
        Stop stop = new Stop(rb);
        RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
        UpdateAnimation updateAnimation = new UpdateAnimation(animator, "Idle");
        sequence.Add(inRnage);
        sequence.Add(getClosest);
        sequence.Add(stop);
        sequence.Add(rotateTowardsTarget);
        sequence.Add(updateAnimation);
        if (debug)
        {
            return AttachDebugLog(sequence, "Look At Player", BehaviourResult.Success);
        }
        return sequence;
    }

    public BTNode AttachDebugLog(BTNode inputNode, string label, BehaviourResult expectedResult)
    {
        inputNode = new DebugLogDecorator
        {
            child = inputNode,
            label = label,
            expectedResult = expectedResult
        };
        return inputNode;
    }



    // Debug draw that we don't really care
    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.rotation.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            //Debug.DrawLine(eyeTransform.position, eyeTransform.position + DirFromAngle(angle, true) * viewRange, Color.red);
            ViewCastInfo viewCast = ViewCast(angle);
            if (i > 0)
            {
               bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - viewCast.dst) > edgeDistanceThreshold;
               if (oldViewCast.hit != viewCast.hit || (oldViewCast.hit && viewCast.hit && edgeDstThresholdExceeded))
               {
                    EdgeInfo edge = FindEdge(oldViewCast, viewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }

                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }
            viewPoints.Add(viewCast.point);
            oldViewCast = viewCast;
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = transform.InverseTransformPoint(eyeTransform.position);
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(eyeTransform.position, dir, out hit, viewRange, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, eyeTransform.position + dir * viewRange, viewRange, globalAngle);
        }
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo viewCast = ViewCast(angle);
            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - viewCast.dst) > edgeDistanceThreshold;

            if (viewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = viewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = viewCast.point;
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;
        public ViewCastInfo(bool hit, Vector3 point, float dst, float angle)
        {
            this.hit = hit;
            this.point = point;
            this.dst = dst;
            this.angle = angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 pointA, Vector3 pointB)
        {
            this.pointA = pointA;
            this.pointB = pointB;
        }
    }

    Vector3 DirFromAngle(float angleInDegree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegree += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
    }


}
