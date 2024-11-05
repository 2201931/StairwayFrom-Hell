using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;
#endif

public class SceneChangeBounds : MonoBehaviour
{
    public GameObject player;                // Reference to the Player GameObject
    public Vector3 zoneCenter;               // Center of the scene change zone
    public Vector3 zoneSize;                 // Size of the scene change zone
    [SerializeField] public string sceneToLoad; // Scene name to load

    private Bounds zoneBounds;
    private bool hasLoadedScene = false;     // Prevent multiple loads

    void Start()
    {
        // Initialize the bounds based on the center and size
        zoneBounds = new Bounds(transform.position + zoneCenter, zoneSize);
    }

    void Update()
    {
        // Update bounds in case of any changes in the Inspector
        zoneBounds.center = transform.position + zoneCenter;

        // Check if the player's position is within the bounds
        if (player != null && zoneBounds.Contains(player.transform.position) && !hasLoadedScene)
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

    void OnDrawGizmosSelected()
    {
        // Draw the zone in the Scene view for visualization
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + zoneCenter, zoneSize);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SceneChangeBounds))]
public class SceneChangeBoundsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SceneChangeBounds script = (SceneChangeBounds)target;

        // Dropdown for selecting scenes
        List<string> sceneNames = new List<string>(GetAllSceneNames());
        int selectedIndex = sceneNames.IndexOf(script.sceneToLoad);

        // Show dropdown to select a scene
        selectedIndex = EditorGUILayout.Popup("Scene To Load", selectedIndex, sceneNames.ToArray());
        script.sceneToLoad = selectedIndex >= 0 ? sceneNames[selectedIndex] : "";

        // Show fields for the zone center and size
        script.zoneCenter = EditorGUILayout.Vector3Field("Zone Center", script.zoneCenter);
        script.zoneSize = EditorGUILayout.Vector3Field("Zone Size", script.zoneSize);

        // Save changes if anything changed
        if (GUI.changed)
        {
            EditorUtility.SetDirty(script);
        }
    }

    // Method to get the names of all scenes in the project
    private string[] GetAllSceneNames()
    {
        List<string> sceneNames = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
                sceneNames.Add(sceneName);
            }
        }
        return sceneNames.ToArray();
    }
}
#endif
