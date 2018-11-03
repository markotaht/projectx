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

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        jumpModifiers = new JumpModifiers();
	}
	
	// Update is called once per frame
	void Update () {
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpModifiers.ApplyModifiers(jumpforce)));
        }
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        float move = rb.velocity.x / maxSpeed;
        if (grounded)
        {
            move = Input.GetAxis("Horizontal");
        }

        rb.velocity = new Vector3(move * maxSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionDir = collision.transform.position - transform.position;
        collisionDir.Normalize();

        RaycastHit2D hitCeiling = Physics2D.BoxCast(transform.position, GetComponent<BoxCollider2D>().size, 0, Vector2.up, 1.2f);

        if(!grounded && collisionDir.y > 0 && hitCeiling.collider != null)
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
            //Ceiling death animation
        }
        else
        {
            //Wall death animation
        }
    }
}
