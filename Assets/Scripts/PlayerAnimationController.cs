using UnityEngine;
using UnityEngine.UI;

public class FlipbookAnimationController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerJump playerJump;

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
        playerJump = GetComponent<PlayerJump>();

        if (playerJump == null)
        {
            Debug.LogError("PlayerJump not found on the GameObject.");
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on the GameObject or its children.");
        }

        Debug.Log("Starting FlipbookAnimationController. Checking sprite arrays...");
        if (idleSprites == null || idleSprites.Length == 0)
        {
            Debug.LogError("Idle sprites array is not assigned or empty.");
        }
        if (walkSprites == null || walkSprites.Length == 0)
        {
            Debug.LogError("Walk sprites array is not assigned or empty.");
        }
        if (chargeSprites == null || chargeSprites.Length == 0)
        {
            Debug.LogError("Charge sprites array is not assigned or empty.");
        }
        if (jumpSprites == null || jumpSprites.Length == 0)
        {
            Debug.LogError("Jump sprites array is not assigned or empty.");
        }
        if (fallSprites == null || fallSprites.Length == 0)
        {
            Debug.LogError("Fall sprites array is not assigned or empty.");
        }
    }

    void Update()
    {
        if (playerJump == null) return;

        // Log player states for debugging
        Debug.Log($"Player grounded: {playerJump.isGrounded}");
        Debug.Log($"Player charging: {playerJump.isCharging}");
        Debug.Log($"Player velocity: {playerJump.rb.velocity}");

        // Determine the correct animation based on `PlayerJump`'s state
        if (!playerJump.isGrounded)
        {
            if (playerJump.rb.velocity.y > 0)
            {
                currentAnimation = jumpSprites;  // Ascending
                Debug.Log("Playing jump animation");
            }
            else
            {
                currentAnimation = fallSprites;  // Falling
                Debug.Log("Playing fall animation");
            }
        }
        else if (playerJump.isCharging)
        {
            currentAnimation = chargeSprites;   // Charging jump
            Debug.Log("Playing charge animation");
        }
        else if (Mathf.Abs(playerJump.rb.velocity.x) > 0.1f)
        {
            currentAnimation = walkSprites;     // Walking
            Debug.Log("Playing walk animation");
        }
        else
        {
            currentAnimation = idleSprites;     // Idle
            Debug.Log("Playing idle animation");
        }

        // Flip sprite based on movement direction
        if (playerJump.rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false; // Facing right
            Debug.Log("Facing right");
        }
        else if (playerJump.rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true; // Facing left
            Debug.Log("Facing left");
        }

        // Play the selected animation sequence
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
            Debug.Log("Current frame: " + currentFrame);
        }
    }
}
