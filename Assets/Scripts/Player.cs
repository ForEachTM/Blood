
using UnityEngine;

public class Player : MonoBehaviour
{
    public GunManager gunManager;

    public Animator animator;

    const float radious = 0.01f;

    public float jumpTime;

    public float jumpForce;
    public float speed;

    public Transform feetPose;
    public LayerMask ground;

    private new Rigidbody2D rigidbody2D;

    private BoxCollider2D capsuleCollider2D;

    private float horizontalMoveInput;

    private float jumpTimeCounter;

    private bool isGrounded;

    private bool dead;
    
    [HideInInspector]
    public bool facingRight;

    public SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
            horizontalMoveInput = Input.GetAxisRaw("Horizontal");
            transform.position = new Vector2(Mathf.Round(transform.position.x * 10) * 0.1f, Mathf.Round(transform.position.y * 10) * 0.1f);
            isGrounded = Physics2D.OverlapCircle(feetPose.position, radious, ground);

            animator.SetFloat("velocity", Mathf.Abs(horizontalMoveInput));
            animator.SetBool("isJumping", !isGrounded);
        
            Flip();
    }

    public void Knockback(float knockback, Vector3 position)
    {
        dead = true;

        Vector2 moveDirection = transform.position - position;

        rigidbody2D.AddForce(moveDirection.normalized * knockback, ForceMode2D.Impulse);
        rigidbody2D.AddTorque(knockback*10f);
    }

    /*public void Knockback(float force)
    {
        transform.position = new Vector2(transform.position.x + force * transform.localScale.x *-1, transform.position.y);
    }*/

    private void FixedUpdate()
    {
        //transform.position = new Vector2(transform.position.x + speed * horizontalMoveInput * Time.fixedDeltaTime, transform.position.y);

        if (!dead)
        {
            rigidbody2D.velocity = new Vector2(horizontalMoveInput * speed, rigidbody2D.velocity.y);
        }

        if (isGrounded && Input.GetButton("Jump"))
        {
            isGrounded = false;
            rigidbody2D.velocity = Vector2.up * jumpForce;
        }

    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag.Equals("Crate"))
        {
            gunManager.SetRandomGun();
            collider2D.gameObject.GetComponent<Crate>().Destroy();
        }

        if (collider2D.gameObject.tag.Equals("Enemy"))
        {
            rigidbody2D.freezeRotation = false;
            capsuleCollider2D.isTrigger = true;
            Knockback(20f, collider2D.gameObject.transform.position);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Enemy"))
        {
            rigidbody2D.freezeRotation = false;
            capsuleCollider2D.isTrigger = true;
            Knockback(5f, collision2D.gameObject.transform.position);
        }
    }

    private void Flip()
    {
        if (horizontalMoveInput > 0 && facingRight || horizontalMoveInput < 0 && !facingRight)
        {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

}
