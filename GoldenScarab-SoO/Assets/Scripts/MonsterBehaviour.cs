using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehaviour : MonoBehaviour
{
    //public MonsterMasterController monsterMasterController;
    [Header("Stop Sequence")]
    public float stopRange;


    [Header("Seek Sequence")]
    public LayerMask playerMask;
    public Transform eyeTransform;
    public LayerMask obstacleMask;
    public float seekThreshold;
    public float rotateSpeed;
    public float viewRange;
    public float viewAngle;
    public float moveForce;

    public List<WayPoints> paths;

    [Header("Look At Player Sequence")]
    public float lookAtPlayerRange;


    [Header("Debug")]
    public bool debug;
    public float meshResolution;
    public float edgeDistanceThreshold;
    public float edgeResolveIterations;
    public MeshFilter viewMeshFilter;


    private NavMeshAgent m_agent;
    private CharacterRB m_rb;
    private Animator m_animator;
    private TreeNode m_root;
    private Blackboard m_blackboard;

    private Mesh viewMesh;

    Queue<TreeNode> m_queueBefore = new Queue<TreeNode>();
    Queue<TreeNode> m_queueAfter = new Queue<TreeNode>();
    private void Awake()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        m_agent = GetComponent<NavMeshAgent>();
        m_rb = GetComponent<CharacterRB>();
        m_animator = GetComponent<Animator>();

        m_root = ConstructBehaviourTree();
        m_blackboard = new Blackboard();

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    
        foreach (TreeNode node in m_queueBefore)
        {
            Execute(node);
        }
        m_queueBefore.Clear();

        if (Execute(m_root) == BehaviourResult.Failure)
        {
            foreach (TreeNode node in m_queueAfter)
            {
                Execute(node);
            }
        }
        m_queueAfter.Clear();

        if (debug)
        {
            DrawFieldOfView();
        }
    }

    public BehaviourResult Execute(TreeNode node)
    {
        return node.Execute(gameObject, m_blackboard, Time.deltaTime);
    }

    public void EnqueueBefore(TreeNode node)
    {
        m_queueBefore.Enqueue(node);
    }

    public void EnqueueAfter(TreeNode node)
    {
        m_queueAfter.Enqueue(node);
    }

    TreeNode ConstructBehaviourTree()
    {
        SelectorNode monsterBehaviour = new SelectorNode();

        // Stop Sequence
        TreeNode stopSequence = CreateStopSequence();

        // Seek Sequence
        TreeNode seekPlayerSequence = CreateSeekPlayerSequence();

        // Stop at last known position
        TreeNode stopAtLastKnownPositionSequence = CreateStopLastKnownPositionSequence();

        // Seek last known position
        TreeNode seekLastKnownPositionSequence = CreateSeekLastSawPositionSequence();

        // Look at player
        TreeNode lookAtPlayerSequence = CreateLookAtPlayerSequence();

        // Move to path
        TreeNode moveToPath = CreateIdlePathSequence();

        monsterBehaviour.Add(stopSequence);
        monsterBehaviour.Add(seekPlayerSequence);
        monsterBehaviour.Add(stopAtLastKnownPositionSequence);
        monsterBehaviour.Add(seekLastKnownPositionSequence);
        //monsterBehaviour.Add(moveToPath);
        monsterBehaviour.Add(lookAtPlayerSequence);

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
        LoseGame loseGame = new LoseGame();
        sequence.Add(inRnage);
        sequence.Add(getClosest);
        sequence.Add(stop);
        sequence.Add(rotateTowardsTarget);
        sequence.Add(updateAnimation);
        sequence.Add(loseGame);

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
        StoreOutputDecorator storeClosestTarget = new StoreOutputDecorator(getClosest, BlackboardKey.LastKnownPosition);
        GetNextWaypoint getNextWayPoint = new GetNextWaypoint(m_agent, BlackboardKey.Input, seekThreshold);
        SeekTarget seekTarget = new SeekTarget(m_rb, BlackboardKey.Input, moveForce);
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

    public TreeNode CreateStopLastKnownPositionSequence()
    {
        SequenceNode sequence = new SequenceNode();
        TreeNode removeStorageDecorator = new DeleteMemoryDecorator(sequence, BlackboardKey.LastKnownPosition, BehaviourResult.Success);
        IsTargetInRange isTargetInRange = new IsTargetInRange(BlackboardKey.LastKnownPosition, stopRange);
        Stop stop = new Stop(m_rb);
        UpdateAnimation updateAnimation = new UpdateAnimation(m_animator, "Idle");
        sequence.Add(isTargetInRange);
        sequence.Add(stop);
        sequence.Add(updateAnimation);

        //if (debug)
        //{
        //    return AttachDebugLog(removeStorageDecorator, "Stop At Last Saw Position", BehaviourResult.Success);
        //}
        return removeStorageDecorator;
    }

    public TreeNode CreateSeekLastSawPositionSequence()
    {
        SequenceNode sequence = new SequenceNode();
        GetNextWaypoint getNextWayPoint = new GetNextWaypoint(m_agent, BlackboardKey.LastKnownPosition, seekThreshold);
        SeekTarget seekTarget = new SeekTarget(m_rb, BlackboardKey.Input, moveForce);
        RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
        UpdateAnimation updateAnimation = new UpdateAnimation(m_animator, "Running");
        sequence.Add(getNextWayPoint);
        sequence.Add(seekTarget);
        sequence.Add(rotateTowardsTarget);
        sequence.Add(updateAnimation);
        //if (debug)
        //{
        //    return AttachDebugLog(sequence, "Seek Last Saw Position", BehaviourResult.Success);
        //}
        return sequence;
    }

    public TreeNode CreateLookAtPlayerSequence()
    {
        SequenceNode sequence = new SequenceNode();
        GetTargetsInRange inRnage = new GetTargetsInRange(playerMask, lookAtPlayerRange);
        GetClosest getClosest = new GetClosest(BlackboardKey.Input);
        Stop stop = new Stop(m_rb);
        RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
        UpdateAnimation updateAnimation = new UpdateAnimation(m_animator, "Idle");
        Fail fail = new Fail();
        sequence.Add(inRnage);
        sequence.Add(getClosest);
        sequence.Add(stop);
        sequence.Add(rotateTowardsTarget);
        sequence.Add(updateAnimation);
        sequence.Add(fail);
        //if (debug)
        //{
        //    return AttachDebugLog(sequence, "Look At Player", BehaviourResult.Success);
        //}
        return sequence;
    }

    public TreeNode CreateIdlePathSequence()
    {
        SequenceNode sequence = new SequenceNode();
        GetNextWayPointOnPath getNextWayPointOnPath = new GetNextWayPointOnPath();
        GetNextWaypoint getNextWaypoint = new GetNextWaypoint(m_agent, BlackboardKey.Input, seekThreshold);
        SeekTarget seekTarget = new SeekTarget(m_rb, BlackboardKey.Input, moveForce);
        RotateTowardsTarget rotateTowardsTarget = new RotateTowardsTarget(BlackboardKey.Input, rotateSpeed);
        UpdateAnimation updateAnimation = new UpdateAnimation(m_animator, "Running");
        sequence.Add(getNextWayPointOnPath);
        sequence.Add(getNextWaypoint);
        sequence.Add(seekTarget);
        sequence.Add(rotateTowardsTarget);
        sequence.Add(updateAnimation);
        //if (debug)
        //{
        //    return AttachDebugLog(sequence, "Look At Player", BehaviourResult.Success);
        //}
        return sequence;
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
