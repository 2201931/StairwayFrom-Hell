using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeBounds : MonoBehaviour
{
    public GameObject player;                // Reference to the Player GameObject
    public Vector2 zoneCenter;               // Center of the scene change zone
    public Vector2 zoneSize;                 // Size of the scene change zone
    [SerializeField] public string sceneToLoad; // Scene name to load

    private Bounds zoneBounds;
    private bool hasLoadedScene = false;     // Prevent multiple loads

    void Start()
    {
        // Initialize the bounds based on the center and size
        zoneBounds = new Bounds((Vector2)transform.position + zoneCenter, zoneSize);
    }

    void Update()
    {
        // Update bounds in case of any changes in the Inspector
        zoneBounds.center = (Vector2)transform.position + zoneCenter;

        // Check if the player's position is within the bounds
        if (player != null && zoneBounds.Contains((Vector2)player.transform.position) && !hasLoadedScene)
        {
            Debug.Log("Player entered the zone. Loading scene: " + sceneToLoad);
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            hasLoadedScene = true;  // Set flag to prevent reloading
            SceneManager.LoadScene(sceneToLoad); // Load the specified scene by name
        }
        else
        {
            Debug.LogError("Scene name is empty. Please set it in the inspector.");
        }
    }

    public void ResetSceneLoad()
    {
        hasLoadedScene = false;  // Reset the flag to allow scene loading again
    }

    void OnDrawGizmosSelected()
    {
        // Draw the zone in the Scene view for visualization
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + zoneCenter, zoneSize);
    }
}
