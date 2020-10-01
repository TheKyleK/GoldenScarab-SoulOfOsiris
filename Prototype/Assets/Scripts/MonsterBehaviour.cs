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

    [Header("Debug Draw")]
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDistanceThreshold;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    BTNode root;
    CharacterRB rb;
    Animator animator;
    NavMeshAgent agent;
    Blackboard blackboard;
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

        

        ConstructBehaviourTree();
    }

    // Update is called once per frame
    void Update()
    {
        root.Execute(gameObject, blackboard, Time.deltaTime);
        DrawFieldOfView();
    }

    void ConstructBehaviourTree()
    {
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
        //RepeatDecorator runOnce = new RepeatDecorator(0.2f, 0.2f, 500);
        DeleteMemoryDecorator removeOutputDecorator = new DeleteMemoryDecorator(BlackboardKey.Input, BehaviourResult.Success);
        removeOutputDecorator.child = monsterBehaviour;
        //runOnce.child = removeOutputDecorator;
        root = removeOutputDecorator;
    }

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
