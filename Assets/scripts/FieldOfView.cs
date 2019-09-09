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


    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
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

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            //THis is to keep the angle relative to the player
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

}
