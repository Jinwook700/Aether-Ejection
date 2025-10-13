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
    private BoxCollider2D boxCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
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
        Vector2 groundCheckPos = boxCollider.bounds.center + Vector3.down * (boxCollider.bounds.extents.y - 0.05f);
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
    }
    public void JumpMove()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector3.up * jumpScale, ForceMode2D.Impulse);
    }
}

