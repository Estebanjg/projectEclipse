using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool isGrounded;
    public Transform GroundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    float yVelocity;
    float bigFallVel;
    private bool facingDirection = true; // Change later for left room transitions (this will always make the Character face right even when entering a room facing left) 
    //facingDirection key: true is right and false is left.

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, whatIsGround);
        yVelocity = rb.velocity.y;
        bigFallVel = Mathf.Abs(yVelocity);

        if(isGrounded == true && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if(Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        
        fallCheck();
        jumpCheck();
    }


    private void flipController()
    {
        // If the input is moving the player right and the player is facing left...
        if (moveInput > 0 && !facingDirection)
			{
				// ... flip the player.
				flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
		else if (moveInput < 0 && facingDirection)
			{
				// ... flip the player.
				flip();
			}
    }
    private void flip()
	{
		// Switch the way the player is labelled as facing.
		facingDirection = !facingDirection;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }
    private void fallCheck()
    {
        if(isGrounded == false)
        {
            animator.SetBool("Falling", true);
        }
        if(isGrounded == true)
        {
            animator.SetBool("Falling", false);
        }
        animator.SetFloat("yVelocity", yVelocity);
        Debug.Log(yVelocity); /* Enters Big Fall Velocity to log */

        animator.SetFloat("BigFallVelocity", bigFallVel);

    }
    private void jumpCheck()
    {
        if(Input.GetButtonUp("Jump"))
        {
            animator.SetBool("IsJumping", false);
        }
        if(Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsJumping", true);
        }
    }
}
