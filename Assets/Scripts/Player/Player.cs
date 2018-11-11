
using UnityEngine;
using UnityEngine.UI;

public class Player : AObject
{
    public float jumpForce;

    public GameObject gameOverPanel;

    float horizontalMoveInput;

    private float knockbackTimer;
    private readonly float knockbackBtwTime = 0.1f;

    public float Knockback { get; set; }

    public int Score { get; set; }

    public Text scoreText;

    void Start()
    {
        scoreText.text = "" + Score;
    }

    void Update()
    {
        horizontalMoveInput = Input.GetAxisRaw("Horizontal");

        animator.SetBool("isJumping", !isCircleCollided);
        animator.SetFloat("velocity", Mathf.Abs(horizontalMoveInput));
    }

    public void GunKnockback(float force)
    {
        knockbackTimer = knockbackBtwTime;
        transform.position = new Vector2(transform.position.x + force * transform.localScale.x *-1, transform.position.y);
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(horizontalMoveInput * speed - Knockback * gameObject.transform.localScale.x, rigidbody2D.velocity.y);

        if (isCircleCollided && Input.GetButton("Jump"))
        {
            isCircleCollided = false;

            rigidbody2D.velocity = Vector2.up * jumpForce;
        }
    }

    public override void Flip()
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
