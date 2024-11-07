using UnityEngine;
using UnityEngine.UI;

public class PlayerJump : MonoBehaviour
{
    public float maxCharge = 10f;  // Maximum charge for jump power
    public float chargeRate = 0.7f; // How fast the jump charge builds up
    public Image chargeMeter;     // UI element to display charge
    public LineRenderer directionIndicator; // Shows jump direction
    public float moveSpeed = 15f;  // Speed of left/right movement
    public float lineZIndex = 0f; // Public z-index for the line renderer
    public float smallJumpForce = 10f; // Force for the small jump

    public Rigidbody2D rb;
    private float currentCharge = 0f;
    public bool isCharging = false;
    public bool isGrounded = false; // Track if player is on a platform
    private Vector2 startMousePosition;
    private Vector2 endMousePosition;
    private int jumpMode;
    public int jumpCount = 0;

    void Start()
    {
        Debug.Log("Start method called. Initializing components.");
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.freezeRotation = true; // Prevents the player from falling over
        chargeMeter.fillAmount = 0f; // Start with an empty charge meter
        directionIndicator.positionCount = 2; // Start and end point for the line
        jumpMode = PlayerPrefs.GetInt("JumpMode", 1); // Default to hard mode
        Debug.Log("Jump Mode: " + jumpMode); // Log the jump mode
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanJump())
        {
            Debug.Log("Mouse button down. Starting charge.");
            // Start charging
            isCharging = true;
            currentCharge = 0f;
            startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && isCharging)
        {
            // Ensure the line renderer has enough positions
            if (directionIndicator.positionCount < 2)
            {
                directionIndicator.positionCount = 2;
            }

            // Calculate distance and update charge
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector2.Distance(startMousePosition, currentMousePosition);
            currentCharge = Mathf.Min(distance, maxCharge); // Limit to max charge
            chargeMeter.fillAmount = currentCharge / maxCharge;  // Update charge meter

            // Update direction indicator
            Vector2 direction = (currentMousePosition - startMousePosition).normalized;
            directionIndicator.SetPosition(0, new Vector3(transform.position.x, transform.position.y, lineZIndex)); // Start point with z-index
            directionIndicator.SetPosition(1, new Vector3(transform.position.x + direction.x * currentCharge, transform.position.y + direction.y * currentCharge, lineZIndex)); // End point with z-index
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            Debug.Log("Mouse button up. Releasing jump.");
            // Release jump
            endMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector2.Distance(startMousePosition, endMousePosition);
            Vector2 direction = (endMousePosition - startMousePosition).normalized;

            // Apply force based on distance
            float forceMagnitude = Mathf.Min(distance * 100f, maxCharge * 200f); // Adjust max force as needed
            rb.AddForce(direction * forceMagnitude);

            // Reset charge and UI
            currentCharge = 0f;
            chargeMeter.fillAmount = 0f;
            directionIndicator.positionCount = 0; // Hide the direction indicator
            isCharging = false;

            // Update jump count correctly
            jumpCount++;
            Debug.Log("Jump count: " + jumpCount);
        }

        // Small jump logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Space bar pressed. Performing small jump.");
            rb.AddForce(Vector2.up * smallJumpForce, ForceMode2D.Impulse);
            isGrounded = true; // Set isGrounded to true when performing small jump
        }

        // Movement logic
        if (isGrounded)
        {
            Debug.Log("Player is grounded, checking for movement input...");
            float move = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            Debug.Log("Move value: " + move);
            transform.Translate(move, 0, 0);
        }
    }

    bool CanJump()
    {
        if (jumpMode == 0 && jumpCount < 2) // Easy mode
        {
            return true;
        }
        else if (jumpMode == 1 && jumpCount < 1) // Hard mode
        {
            return true;
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            // Check if the collision normal is pointing upwards
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    Debug.Log("Player has landed on a platform.");
                    jumpCount = 0; // Reset jump count when grounded
                    break;
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            // Check if the collision normal is pointing upwards
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Collision exit detected with: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            isGrounded = false;
            Debug.Log("Player has left the platform.");
        }
    }
}
