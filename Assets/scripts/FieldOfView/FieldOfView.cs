using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;
    public int edgeResolveIterations;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }


    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        
        List<Collider2D> results = new List<Collider2D>(0);
        ContactFilter2D contact = new ContactFilter2D();
        contact.SetLayerMask(targetMask);

        int numberOfTargets = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), viewRadius, contact, results); 

        //Loop through all the targets in our circle of vision
        for (int i=0; i< numberOfTargets; i++)
        {
            Transform target = results[i].transform;
            //Get the direction towards the target
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            //Check if the target is within our view angle of vision
            if (Vector2.Angle (transform.up, dirToTarget) < viewAngle / 2)
            {
                   float distToTarget = Vector3.Distance(transform.position, target.position);

                   if (!Physics2D.Raycast (transform.position, dirToTarget, distToTarget, obstacleMask)){
                        visibleTargets.Add(target);
                   }
            }
        }

    }
 

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3> ();
        ViewCastInfo oldViewCast = new ViewCastInfo();


        
        for (int i =0; i <= stepCount; i++)
        {
            float angle = (transform.eulerAngles.z*-1) - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCastInfo = ViewCast(angle);

            if (i > 0)
            {
                if (oldViewCast.hit != newViewCastInfo.hit)
                {
                    //Debug.Log("Do we enter?");
                    /*EdgeInfo edge = FindEdge(oldViewCast, newViewCastInfo);
                    if (edge.pointA != Vector2.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector2.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }*/
                }
            }

            viewPoints.Add(newViewCastInfo.point);
            oldViewCast = newViewCastInfo;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector2.zero;
        for (int i =0; i< vertexCount - 1; i++)
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);
        if (hit.collider != null)
        {
            return new ViewCastInfo(hit, hit.point, hit.distance, hit.collider.transform.eulerAngles.z); 
        } else
        {
            return new ViewCastInfo(hit, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }


    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(RaycastHit2D _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {

        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    public struct EdgeInfo
    {
        public Vector2 pointA;
        public Vector2 pointB;
        public EdgeInfo(Vector2 _pointA, Vector2 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector2 minPoint = Vector2.zero;
        Vector2 maxPoint = Vector2.zero;
        Debug.Log("Previous angles (min/max) : " + minAngle + ":" + maxAngle);

        for (int i = 0; i< edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2f;
            Debug.Log("New angle to try: " + minAngle + " + " + maxAngle + " / 2 = " + angle);
            ViewCastInfo newViewCast = ViewCast(angle);

            if(newViewCast.hit == minViewCast.hit)
            {
                minAngle = angle;
                maxPoint = newViewCast.point;
            } else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }

}
