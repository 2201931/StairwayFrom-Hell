using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneChangeTrigger : MonoBehaviour
{
    [SerializeField] public Object sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.CompareTag("Player"))
        {
            if (sceneToLoad != null)
            {
                // Log scene name and loading action for debugging
                Debug.Log("Player entered trigger, attempting to load scene: " + sceneToLoad.name);

                // Load the scene using its name
                SceneManager.LoadScene(sceneToLoad.name);
            }
            else
            {
                Debug.LogError("Scene to load is not assigned in the inspector!");
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SceneChangeTrigger))]
public class SceneChangeTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SceneChangeTrigger script = (SceneChangeTrigger)target;

        // Show a scene field that accepts only scene assets
        script.sceneToLoad = EditorGUILayout.ObjectField("Scene To Load", script.sceneToLoad, typeof(SceneAsset), false);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(script);
        }
    }
}
#endif
