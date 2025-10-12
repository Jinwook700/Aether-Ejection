using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpScale = 7f;

    public Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        HorizontalMove();
        JumpMove();
    }
    public void HorizontalMove()
    {
        float horizontalDir = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontalDir * moveSpeed, rb.velocity.y);
    }
    public void JumpMove()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpScale, ForceMode2D.Impulse);
        }
    }
}

