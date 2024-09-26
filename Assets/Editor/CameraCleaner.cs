using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class CameraCleaner : EditorWindow, IPreprocessBuildWithReport
{
    private static bool enableCameraCleaning;

    [MenuItem("Tools/Camera Cleaner")]
    public static void ShowWindow()
    {
        GetWindow<CameraCleaner>("Camera Cleaner");
        LoadPreferences();
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        LoadPreferences();
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnGUI()
    {
        GUILayout.Label("Camera Cleaner Settings", EditorStyles.boldLabel);

        // Toggle to enable/disable camera cleaning
        bool newEnableCameraCleaning = EditorGUILayout.Toggle("Enable Camera Cleaning", enableCameraCleaning);

        if (newEnableCameraCleaning != enableCameraCleaning)
        {
            enableCameraCleaning = newEnableCameraCleaning;
            SavePreferences();
        }

        if (GUILayout.Button("Delete Non-Main Cameras Now"))
        {
            DeleteNonMainCamerasInAllScenes();
        }
    }

    private static void LoadPreferences()
    {
        enableCameraCleaning = EditorPrefs.GetBool("CameraCleaner_EnableCameraCleaning", false);
    }

    private static void SavePreferences()
    {
        EditorPrefs.SetBool("CameraCleaner_EnableCameraCleaning", enableCameraCleaning);
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode && enableCameraCleaning)
        {
            Debug.Log("CameraCleaner: Cleaning cameras on Play Mode start.");
            DeleteNonMainCamerasInCurrentScene();
        }
    }

    public static void DeleteNonMainCamerasInAllScenes()
    {
        if (!enableCameraCleaning)
        {
            Debug.Log("Camera cleaning is disabled. No cameras were deleted.");
            return;
        }

        // Only perform this action if not in Play Mode
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            Debug.LogWarning("Cannot clean cameras in all scenes during Play Mode. Use the button in Edit Mode or during build.");
            return;
        }

        // Get current active scene to return to it later
        Scene currentActiveScene = SceneManager.GetActiveScene();

        // Iterate through all scenes in the Build Settings
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            Scene scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

            DeleteNonMainCamerasInScene(scene);

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }

        // Return to the original active scene
        EditorSceneManager.OpenScene(currentActiveScene.path, OpenSceneMode.Single);

        Debug.Log("Deleted all non-main cameras in all scenes.");
    }

    public static void DeleteNonMainCamerasInCurrentScene()
    {
        if (!enableCameraCleaning)
        {
            Debug.Log("Camera cleaning is disabled. No cameras were deleted.");
            return;
        }

        Scene currentScene = SceneManager.GetActiveScene();
        DeleteNonMainCamerasInScene(currentScene);

        Debug.Log("Deleted all non-main cameras in the current scene.");
    }

    private static void DeleteNonMainCamerasInScene(Scene scene)
    {
        Camera[] allCameras = GameObject.FindObjectsOfType<Camera>();

        foreach (Camera cam in allCameras)
        {
            if (cam == Camera.main || IsChildOfXROrigin(cam.gameObject))
            {
                continue; // Skip the main camera and XR Origin children
            }

            DestroyImmediate(cam.gameObject);
        }
    }

    private static bool IsChildOfXROrigin(GameObject obj)
    {
        Transform current = obj.transform;
        while (current != null)
        {
            if (current.name == "XR Origin") // Check if any parent is named "XR Origin"
            {
                return true;
            }
            current = current.parent;
        }
        return false;
    }

    // Implement the interface to add build preprocessing
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        if (enableCameraCleaning)
        {
            Debug.Log("CameraCleaner: Cleaning cameras before build.");
            DeleteNonMainCamerasInAllScenes();
        }
        else
        {
            Debug.Log("CameraCleaner: Skipping camera cleaning before build.");
        }
    }
}
