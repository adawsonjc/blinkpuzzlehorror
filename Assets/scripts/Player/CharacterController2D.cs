using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement

	public bool isMoving;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private Vector3 m_Velocity = Vector3.zero;
    
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }


	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{

		
	}

   
    public void Move(float moveHorizontal, float moveVertical)

    {
       
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(moveHorizontal * 10f, moveVertical * 10f);

        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // Sets isMoving to true when the distance between current velocity and target 
        // velocity is greater than 0.1
        isMoving = Vector3.Distance(m_Velocity, targetVelocity) > 0.1f ? true : false;
    }


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		//m_FacingRight = !m_FacingRight;
		// Multiply the player's x local scale by -1.
		//Vector3 theScale = transform.localScale;
		//theScale.x *= -1;
		//transform.localScale = theScale;
        //MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();

    }
}
