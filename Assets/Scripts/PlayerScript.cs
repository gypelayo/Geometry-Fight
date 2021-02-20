using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float moveForce = 10;
    [SerializeField]
    private float jumpForce = 10;
    private bool canJump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HorizontalMovementHandler();
        canJump = isGrounded();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (canJump)
            {
                Jump();
            }
        }
    }

    /// <summary>
    /// Method to verify if the player is grounded 
    /// </summary>
    /// <returns></returns>
    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f);

        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Method to handle horizontal input
    /// </summary>
    private void HorizontalMovementHandler()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (rb.velocity.x < 5 && rb.velocity.x > -5)
        {
            rb.AddForce(new Vector2(inputHorizontal, 0) * moveForce, ForceMode2D.Impulse);
        }

        if (rb.velocity.x > 5 && inputHorizontal > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
        }

        if (rb.velocity.x < -5 && inputHorizontal < 0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
        }
    }

    /// <summary>
    /// Method to handle jumping
    /// </summary>
    private void Jump()
    {
        rb.AddForce(-Physics.gravity.normalized * jumpForce, ForceMode2D.Impulse);
    }
}
