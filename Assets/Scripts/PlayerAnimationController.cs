using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    // Animation states
    private bool isWalking = false;
    private bool isJumpCharging = false;
    private bool isJumping = false;

    // Jump timing
    public float chargeTime = 0.5f; // Time needed to fully charge jump
    private float chargeCounter = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimatorStates();
    }

    void HandleMovement()
    {
        // Walking logic
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    void HandleJump()
    {
        // Charging the jump
        if (Input.GetButton("Jump"))
        {
            if (!isJumpCharging)
            {
                isJumpCharging = true;
                chargeCounter = 0f;
            }

            chargeCounter += Time.deltaTime;
            if (chargeCounter >= chargeTime)
            {
                chargeCounter = chargeTime;
            }
        }

        // Releasing the jump
        if (Input.GetButtonUp("Jump"))
        {
            if (isJumpCharging)
            {
                isJumpCharging = false;
                isJumping = true;

                // Apply jump force based on chargeCounter (strength of jump)
                float jumpStrength = Mathf.Lerp(5f, 10f, chargeCounter / chargeTime);
                rb.velocity = new Vector2(rb.velocity.x, jumpStrength);

                chargeCounter = 0f;
            }
        }

        // Falling check
        if (rb.velocity.y < 0 && !isJumpCharging && !isJumping)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }
    }

    void UpdateAnimatorStates()
    {
        // Set animator parameters based on state
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isJumpCharging", isJumpCharging);
        animator.SetBool("isJumping", isJumping);

        // Reset isJumping if grounded (simulate grounded check)
        if (isJumping && rb.velocity.y <= 0)
        {
            isJumping = false;
        }
    }
}
