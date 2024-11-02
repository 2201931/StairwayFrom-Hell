using UnityEngine;
using UnityEngine.UI;

public class KeyJump : MonoBehaviour
{
    public float maxCharge = 15f;  // Maximum charge for jump power
    public float chargeRate = 10f; // How fast the jump charge builds up
    public Image chargeMeter;     // UI element to display charge
    public LineRenderer directionIndicator; // Shows jump direction
    public float moveSpeed = 7f;  // Speed of left/right movement

    private Rigidbody2D rb;
    private float currentCharge = 0f;
    private bool isCharging = false;
    private bool isGrounded = false; // Track if player is on a platform
    private Vector2 startMousePosition;
    private Vector2 endMousePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Prevents the player from falling over
        chargeMeter.fillAmount = 0f; // Start with an empty charge meter
        directionIndicator.positionCount = 2; // Start and end point for the line
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Start charging
            isCharging = true;
            currentCharge = 0f;
            startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetKey(KeyCode.Space) && isCharging)
        {
            // Ensure the line renderer has enough positions
            if (directionIndicator.positionCount < 2)
            {
                directionIndicator.positionCount = 2;
            }

            // Increase charge over time
            currentCharge += chargeRate * Time.deltaTime;
            currentCharge = Mathf.Min(currentCharge, maxCharge); // Limit to max charge
            chargeMeter.fillAmount = currentCharge / maxCharge;  // Update charge meter

            // Update direction indicator
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (currentMousePosition - startMousePosition).normalized;
            directionIndicator.SetPosition(0, transform.position);
            directionIndicator.SetPosition(1, (Vector2)transform.position + direction * currentCharge);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            // Release jump
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (currentMousePosition - startMousePosition).normalized;
            rb.AddForce(direction * currentCharge * 100f); // Adjust force multiplier as needed

            // Reset charge and UI
            currentCharge = 0f;
            chargeMeter.fillAmount = 0f;
            directionIndicator.positionCount = 0; // Hide the direction indicator
            isCharging = false;
        }

        // Movement logic
        if (isGrounded)
        {
            float move = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            transform.Translate(move, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platforms"))
        {
            isGrounded = false;
        }
    }
}
