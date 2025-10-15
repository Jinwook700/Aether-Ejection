using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float moveSpeed = 5f;
    public float jumpScale = 1f;

    [Header("Ground Check")]
    public float checkRadius = 0.2f;
    private Vector2 groundCheckSize = new Vector2(0.9f, 0.1f);
    public LayerMask whatIsGround;

    private bool isGrounded;
    private bool jumpRequested = false;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private CapsuleCollider2D capCollider;
    private Animator animator;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
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
        Vector2 point = new Vector2(capCollider.bounds.center.x, capCollider.bounds.min.y);
        Vector2 size = new Vector2(capCollider.size.x * 0.95f, 0.05f);

        isGrounded = Physics2D.OverlapBox(point, size, 0f, whatIsGround);

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
        bool isMoving = Mathf.Abs(horizontalDir) > 0;

        if (horizontalDir > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalDir < 0)
        {
            spriteRenderer.flipX = true;
        }

        animator.SetBool("isMoving", isMoving);
    }
    public void JumpMove()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector3.up * jumpScale, ForceMode2D.Impulse);
    }
}

