using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpScale = 1f;

    [Header("Ground Check")]
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;

    private bool isGrounded;
    private bool jumpRequested = false;

    private Rigidbody2D rb;
    private CapsuleCollider2D capCollider;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 groundCheckPos = capCollider.bounds.center + Vector3.down * (capCollider.bounds.extents.y - 0.05f);
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, checkRadius, whatIsGround);

        HorizontalMove();

        if (jumpRequested && isGrounded)
        {
            JumpMove();
            jumpRequested = false;
        }
        else if (jumpRequested)
        {
            jumpRequested = false;
        }
    }

    public void HorizontalMove()
    {
        float horizontalDir = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalDir * moveSpeed, rb.velocity.y);
        animator.SetFloat("xDir", horizontalDir);
    }
    public void JumpMove()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector3.up * jumpScale, ForceMode2D.Impulse);
    }
}

