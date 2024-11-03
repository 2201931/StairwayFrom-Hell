using System.Collections;
using UnityEngine;

public class FlipbookAnimationController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerJump playerController;

    // Animation sequences
    public Sprite[] idleSprites;
    public Sprite[] walkSprites;
    public Sprite[] chargeSprites;
    public Sprite[] jumpSprites;
    public Sprite[] fallSprites;

    // Frame rate for animations
    public float frameRate = 10f;

    private float frameTimer;
    private int currentFrame;

    private Sprite[] currentAnimation;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on the GameObject.");
        }

        if (playerController == null)
        {
            Debug.LogError("PlayerController not found on the GameObject.");
        }
    }

    void Update()
    {
        if (playerController == null) return;

        // Select the appropriate animation based on player state
        if (playerController.IsFalling)
        {
            currentAnimation = fallSprites;
        }
        else if (playerController.IsJumping)
        {
            currentAnimation = jumpSprites;
        }
        else if (playerController.IsJumpCharging)
        {
            currentAnimation = chargeSprites;
        }
        else if (playerController.IsWalking)
        {
            currentAnimation = walkSprites;
        }
        else
        {
            currentAnimation = idleSprites;
        }

        // Flip sprite based on movement direction
        if (playerController.FacingDirection > 0)
        {
            spriteRenderer.flipX = false; // Facing right
        }
        else if (playerController.FacingDirection < 0)
        {
            spriteRenderer.flipX = true; // Facing left
        }

        // Update the animation frame
        PlayAnimation(currentAnimation);
    }

    void PlayAnimation(Sprite[] animation)
    {
        if (animation == null || animation.Length == 0) return;

        frameTimer += Time.deltaTime;
        if (frameTimer >= 1f / frameRate)
        {
            frameTimer = 0f;
            currentFrame = (currentFrame + 1) % animation.Length;
            spriteRenderer.sprite = animation[currentFrame];
        }
    }
}

