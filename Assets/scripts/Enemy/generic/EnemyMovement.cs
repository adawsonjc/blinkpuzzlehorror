using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] private LastPlayerSighting lastSighting;
    private Rigidbody2D rb;

    private Vector2 velocity = Vector2.zero;
    private float movementSmoothing = 0.05f;
    public float speed = 1.0f;

    private void FixedUpdate()
    {
        


    }

    public void MoveTo(Vector2 v)
    {
        
        // Move the character by finding the target velocity
        Vector2 targetPosition = new Vector2(v.x, v.y);

        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move
        rb.position = Vector2.MoveTowards(rb.position, targetPosition, step);

        // If the input is moving the player right and the player is facing left...


    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        // Multiply the player's x local scale by -1.
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
