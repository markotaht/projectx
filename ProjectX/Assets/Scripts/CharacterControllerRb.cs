using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerRb : MonoBehaviour {

    Rigidbody2D rb;

    [SerializeField]
    private float maxSpeed = 10;

    [SerializeField]
    private float jumpforce = 350f;

    public JumpModifiers jumpModifiers;

    [SerializeField]
    private GameObject trail;

    private Animator animator;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.1f;
    public LayerMask whatIsGround;

    public float maxFartTime = 2f;
    private float currentFartTime = 0f;

    bool facingRight = false;
    bool inAir = false;
    bool farting = false;
    bool canFart = false;

    public void EatFood()
    {
        canFart = true;
        jumpModifiers.FoodEaten += 1;
    }

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        jumpModifiers = new JumpModifiers();
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Jump up", true);
        }

        if(!grounded && Input.GetKeyDown(KeyCode.LeftShift) && canFart)
        {
            canFart = false;
            animator.SetBool("Fart", true);
            farting = true;
            currentFartTime = maxFartTime;
            rb.gravityScale = 0;
            trail.active = true;
        }
    }

    public void Jump()
    {
        animator.SetBool("Jump up", false);
        inAir = true;
        rb.AddForce(new Vector2(0, jumpModifiers.ApplyModifiers(jumpforce)));
    }

    public void Land()
    {
        inAir = false;
        animator.SetBool("Falling", true);
    }

    public void Landed()
    {
        animator.SetBool("Falling", false);
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (farting)
        {
            if(currentFartTime > 0)
            {
                currentFartTime -= Time.deltaTime;
                float move = 100 * (facingRight ? 1 : -1);
                rb.velocity = new Vector2(move, 0);
            }
            else
            {
                rb.gravityScale = 1;
                farting = false;
                animator.SetBool("Fart", false);
                trail.active = false;
            }
        }
        else
        {
            float move = rb.velocity.x / maxSpeed;
            if (grounded && inAir && rb.velocity.y < 0)
            {
                Land();
            }
            else if (grounded)
            {
                move = Input.GetAxisRaw("Horizontal");
                animator.SetFloat("Walking", Mathf.Abs(move));

                if (move > 0 && !facingRight)
                {
                    Flip();
                }
                else if (move < 0 && facingRight)
                {
                    Flip();
                }
            }

            rb.velocity = new Vector3(move * maxSpeed, rb.velocity.y);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!grounded)
        {

            RaycastHit2D hitUp = Physics2D.CapsuleCast(transform.position, GetComponent<CapsuleCollider2D>().size, CapsuleDirection2D.Vertical, 0, Vector2.up, 1.1f);
            RaycastHit2D hitLeft = Physics2D.CapsuleCast(transform.position, GetComponent<CapsuleCollider2D>().size, CapsuleDirection2D.Vertical, 0, Vector2.left, 1.1f);
            RaycastHit2D hitRight = Physics2D.CapsuleCast(transform.position, GetComponent<CapsuleCollider2D>().size, CapsuleDirection2D.Vertical, 0, Vector2.right, 1.1f);

            if (hitLeft.collider != null || hitRight.collider)
            {
                Die(false);
            }
            else if (hitUp.collider != null)
            {

                Die(true);
            }
        }
    }

    private void Die(bool ceiling)
    {
        if (ceiling)
        {
            animator.SetBool("HitCeiling", true);
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            //Ceiling death animation
            Debug.Log("ceiling death");
        }
        else
        {
            animator.SetBool("HitWall", true);
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            Debug.Log("wall death");
        }
    }
}
