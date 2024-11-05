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
    }

    void Update()
    {
        if (playerJump == null) return;

        // Determine the correct animation based on `PlayerJump`'s state
        if (!playerJump.isGrounded)
        {
            if (playerJump.rb.velocity.y > 0)
            {
                currentAnimation = jumpSprites;  // Ascending
            }
            else
            {
                currentAnimation = fallSprites;  // Falling
            }
        }
        else if (playerJump.isCharging)
        {
            currentAnimation = chargeSprites;   // Charging jump
        }
        else if (Mathf.Abs(playerJump.rb.velocity.x) > 0.1f)
        {
            currentAnimation = walkSprites;     // Walking
        }
        else
        {
            currentAnimation = idleSprites;     // Idle
        }

        // Flip sprite based on movement direction
        if (playerJump.rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false; // Facing right
        }
        else if (playerJump.rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true; // Facing left
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
        }
    }
}
