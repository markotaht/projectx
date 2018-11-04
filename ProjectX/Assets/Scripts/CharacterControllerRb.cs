using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerRb : MonoBehaviour {

    Rigidbody2D rb;

	//Liikumiskiirus
    [SerializeField]
    private float maxSpeed = 10;

	//Hüppe jõud
    [SerializeField]
    private float jumpforce = 350f;

	//Peeru tugevus
    [SerializeField]
    private float fartForce = 0.001f;

	//Toidu jõud
    [SerializeField]
    private float foodForce = 0.5f;
	
	//Peeru pikkus
	[SerializeField]
    private float fartDuration = 0.5f;

    [SerializeField]
    private PlayerSoundController psc;

    [SerializeField]
    private DeathScreenController dsc;

    private float jumpModifier = 1;

    [SerializeField]
    private GameObject trail;

    private Animator animator;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    private float currentFartTime = 0f;

    bool facingRight = false;
    bool inAir = false;
    bool farting = false;
    bool canFart = false;
    bool jumping = false;
    bool walking = false;
    bool falling = false;
    bool alive = true;
    public bool won = false;



    public void EatBanana()
    {
        jumpModifier += 1;
    }

    public void EatCheese()
    {
        canFart = true;
    }

    public void EatBeans()
    {
        jumpModifier = Mathf.Max(1, jumpModifier - 1);
    }

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (grounded && Input.GetButtonDown("Jump") && alive && !won)
        {
            animator.SetBool("Jump up", true);
        }

        if(!grounded && Input.GetButton("Fart") && alive && canFart && !won)
        {
            canFart = false;
            animator.SetBool("Fart", true);
            farting = true;
            currentFartTime = fartDuration;
            rb.gravityScale = 0;
            trail.active = true;
        }
    }

    public void Jump()
    {
   
        animator.SetBool("Jump up", false);
        inAir = true;
        jumping = true;
        rb.AddForce(new Vector2(0, jumpforce + jumpforce * jumpModifier * foodForce));
      
    }

    public void Land()
    {
        inAir = false;
        animator.SetBool("Landing", true);
    }

    public void Landed()
    {
        animator.SetBool("Landing", false);
        jumping = false;
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        if (farting)
        {
            if(currentFartTime > 0)
            {
                currentFartTime -= Time.deltaTime;
                float move = fartForce * (facingRight ? 1 : -1);
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
            else if (grounded && alive && !won)
            {
                move = Input.GetAxisRaw("Horizontal");
                if (Mathf.Abs(move) > 0 && !walking)
                {
                    walking = true;
                    psc.Running(true);
                }else if(Mathf.Approximately(move, 0) && walking)
                {
                    walking = false;
                    psc.Running(false);
                }
                animator.SetFloat("Walking", Mathf.Abs(move));

                if (move > 0 && !facingRight)
                {
                    Flip();
                }
                else if (move < 0 && facingRight)
                {
                    Flip();
                }
                rb.velocity = new Vector3(move * maxSpeed, rb.velocity.y);
            }else if (!grounded && !jumping)
            {
                rb.velocity = new Vector2(rb.velocity.x - rb.velocity.x * 0.02f, rb.velocity.y);
            }
            else
            {
                walking = false;
                psc.Running(false); 
            }

           
        }
    }

    public void Win()
    {
        won = true;
        animator.SetFloat("Walking", 0);
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
        if (!grounded && jumping && !farting)
			
        {

            RaycastHit2D hitUp = Physics2D.CapsuleCast(transform.position, GetComponent<CapsuleCollider2D>().size - new Vector2(0.5f,0), CapsuleDirection2D.Vertical, 0, Vector2.up, GetComponent<CapsuleCollider2D>().size.y /2 + 0.1f);
            
            if (hitUp.collider != null)
            {

                Die(true);
            }
        }else if(!grounded && farting)
        {
            RaycastHit2D hitLeft = Physics2D.CapsuleCast(transform.position, GetComponent<CapsuleCollider2D>().size, CapsuleDirection2D.Vertical, 0, Vector2.left, GetComponent<CapsuleCollider2D>().size.x / 2 + 0.1f);
            RaycastHit2D hitRight = Physics2D.CapsuleCast(transform.position, GetComponent<CapsuleCollider2D>().size, CapsuleDirection2D.Vertical, 0, Vector2.right, GetComponent<CapsuleCollider2D>().size.x / 2 + 0.1f);

            if (hitLeft.collider != null || hitRight.collider)
            {
                Die(false);
            }
        }
    }

    private void Die(bool ceiling)
    {
        alive = false;
        psc.Die();
        if (dsc != null)
        {
            dsc.Dead();
        }
        if (ceiling)
        {
            animator.SetBool("HitCeiling", true);
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            farting = false;
            animator.SetBool("Fart", false);
            trail.active = false;
            //Ceiling death animation
            Debug.Log("ceiling death");
        }
        else
        {
            animator.SetBool("HitWall", true);
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            farting = false;
            animator.SetBool("Fart", false);
            trail.active = false;
            Debug.Log("wall death");
        }
        
    }
}
