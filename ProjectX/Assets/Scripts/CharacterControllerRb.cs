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

    private Animator animator;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.1f;
    public LayerMask whatIsGround;

    bool facingRight = false;
    bool inAir = false;

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
       
        float move = rb.velocity.x / maxSpeed;
        if(grounded && inAir && rb.velocity.y < 0)
        {
            Land();
        }
        else if (grounded)
        {
            move = Input.GetAxisRaw("Horizontal");
            animator.SetFloat("Walking", Mathf.Abs(move));

            if(move > 0 && !facingRight)
            {
                Flip();
            }else if(move < 0 && facingRight)
            {
                Flip();
            }
        }

        rb.velocity = new Vector3(move * maxSpeed, rb.velocity.y);
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
        Vector2 collisionDir = collision.transform.position - transform.position;
        collisionDir.Normalize();

        RaycastHit2D hitCeiling = Physics2D.CircleCast(transform.position, GetComponent<CircleCollider2D>().radius, Vector2.up, 1.1f);

        if (!grounded && !Mathf.Approximately(collisionDir.x, 0) && !Mathf.Approximately(rb.velocity.x, 0))
        {
            Die(false);
        }
        else if (!grounded && collisionDir.y > 0 && hitCeiling.collider != null)
        {
            Debug.Log(hitCeiling.collider.name);
            Die(true);
        }
        else if(!grounded && !Mathf.Approximately(collisionDir.x, 0) && !Mathf.Approximately(rb.velocity.x, 0))
        {
            Die(false);
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
