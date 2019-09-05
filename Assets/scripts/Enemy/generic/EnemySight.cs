using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour {

    public float fieldOfViewAngle = 110f;
    public bool playerInSight;

    public Vector2 personalLastSighting;
    public LastPlayerSighting lastPlayerSighting;

    private NavMeshAgent nav;
    private CircleCollider2D col;
    private Animator anim;
    private GameObject player;
    private Animator playerAnim;
    private PlayerHealth playerHealth;
    
    private Vector2 previousSighting;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<Animator>();
        playerHealth = player.GetComponent<PlayerHealth>();

        personalLastSighting = lastPlayerSighting.resetPosition();
        previousSighting = lastPlayerSighting.resetPosition();
    }

    private void Update()
    {
        if (lastPlayerSighting.getPosition() != previousSighting)
            personalLastSighting = lastPlayerSighting.getPosition();

        previousSighting = lastPlayerSighting.getPosition();

    }


    public void OnTriggerStay2D(Collider2D other)
    {

        if(other.gameObject == player)
        {
            playerInSight = false;

            Vector2 direction = other.transform.position - transform.position;
            // float angle = Vector2.Angle(direction, transform.forward);

            // if(angle < fieldOfViewAngle * 0.5f)
            // {
            //   RaycastHit hit;
            //   if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
            //   {
            //       if(hit.collider.gameObject == player)
            playerInSight = true;
            lastPlayerSighting.setPosition(player.transform.position);

            // }
            // }
        }
       
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInSight = false;
    }
    public float CalculatePathLength(Vector2 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (nav.enabled)
            nav.CalculatePath(targetPosition, path);

        Vector2[] allWayPoints = new Vector2[path.corners.Length + 2];

        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for(int i=0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        float pathLength = 0f;

        for(int i=0; i < allWayPoints.Length-1; i++)
        {
            pathLength += Vector2.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }
}
