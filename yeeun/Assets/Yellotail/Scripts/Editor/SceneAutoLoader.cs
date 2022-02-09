using System;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yellotail
{
    /// <summary>
    /// Scene auto loader.
    /// </summary>
    /// <description>

    /// This class adds a Window > Scene Auto Loader menu containing options to select
    /// a "master scene" enable it to be auto-loaded when the user presses play
    /// in the editor. When enabled, the selected scene will be loaded on play,
    /// then the original scene will be reloaded on stop.
    ///
    /// Based on an idea on this thread:
    /// http://forum.unity3d.com/threads/157502-Executing-first-scene-in-build-settings-when-pressing-play-button-in-editor
    /// </description>
    [InitializeOnLoad]
    static class SceneAutoLoader
    {
        // Static constructor binds a playmode-changed callback.
        // [InitializeOnLoad] above makes sure this gets execusted.
        static SceneAutoLoader()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
       
        // Menu items to select the "master" scene and control whether or not to load it.
        [MenuItem("Yellotail Tools/Scene Auto Loader/Select Main Scene...")]
        private static void SelectMainScene()
        {
            string mainScene = EditorUtility.OpenFilePanel("Select Main Scene", Application.dataPath, "unity");
            mainScene = mainScene.Replace(Application.dataPath, "Assets");
            if (!string.IsNullOrEmpty(mainScene))
            {
                MainScene = mainScene;
                LoadMainSceneOnPlay = true;
            }
        }

        [MenuItem("Yellotail Tools/Scene Auto Loader/Load Main On Play", true)]
        private static bool ShowLoadMainSceneOnPlay()
        {
            return !LoadMainSceneOnPlay;
        }
        [MenuItem("Yellotail Tools/Scene Auto Loader/Load Main On Play")]
        private static void EnableLoadMasterOnPlay()
        {
            LoadMainSceneOnPlay = true;
        }

        [MenuItem("Yellotail Tools/Scene Auto Loader/Don't Load Main On Play", true)]
        private static bool ShowDontLoadMainSceneOnPlay()
        {
            return LoadMainSceneOnPlay;
        }
        [MenuItem("Yellotail Tools/Scene Auto Loader/Don't Load Main On Play")]
        private static void DisableLoadMainSceneOnPlay()
        {
            LoadMainSceneOnPlay = false;
        }
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {           
            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                // Check if master scene is part of the current project
                // If the user switches between projects, the persistet "master scene" setting could potentially
                //   point to a scene that exists within another project but not this one
                bool existsMasterSceneInProject = (AssetDatabase.AssetPathToGUID(MainScene) != "");

                // Store an empty list of scenes by default
                ScenesInHierarchyView = new SceneSetup[] { };

                // Only proceed if scene loading is active and the master scene exists in the current project
                if (LoadMainSceneOnPlay && existsMasterSceneInProject)
                {
                    // Save current unsaved scenes if necessary
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        // Persist the current list of scenes in the hierarchy view including their status (not loaded, loaded, active).
                        ScenesInHierarchyView = EditorSceneManager.GetSceneManagerSetup();

                        // User has no unsaved changes at this point; switch to master scene
                        Scene scene = EditorSceneManager.OpenScene(MainScene, OpenSceneMode.Single);

                        // If scene switching failed, then cancel play and switch back to the previous set of scenes
                        if (!scene.IsValid())
                        {
                            EditorApplication.isPlaying = false;
                            EditorApplication.update += ReloadLastScene;
                        }
                    }
                    else
                    {
                        // User cancelled the save operation -- cancel play as well.
                        EditorApplication.isPlaying = false;
                    }
                }                
            }

            if (EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            {
                // User pressed stop
                if (ScenesInHierarchyView.Length != 0)
                {
                    // There is a list of previous scenes available; reload those
                    EditorApplication.update += ReloadLastScene;
                }
            }
        }  
       
        public static void ReloadLastScene()
        {
            if (EditorApplication.isPlaying)
                return;

            SceneSetup[] scenes = ScenesInHierarchyView;

            // Workaround for Unity 5.5.0f3 bug (and possibly in newer versions as well):
            //
            // If the first loaded scene in scenes is not active, then RestoreSceneManagerSetup() will not load that scene.
            // We work around this by ensuring that RestoreSceneManagerSetup() is given a list of scenes where the first loaded scene
            //  always is active, and if necessary switch the active scene to the appropriate one after the call.

            int activeSceneIndex = Array.FindIndex(scenes, scene => scene.isActive);
            int firstLoadedSceneIndex = Array.FindIndex(scenes, scene => scene.isLoaded);

            if (activeSceneIndex != firstLoadedSceneIndex)
            {
                scenes[firstLoadedSceneIndex].isActive = true;
                scenes[activeSceneIndex].isActive = false;
            }

            // Restore old scene list in Hierarchy view - not-loaded, loaded, and active scenes
            EditorSceneManager.RestoreSceneManagerSetup(scenes);

            // Workaround part two: switch to the appropriate active scene
            if (activeSceneIndex != firstLoadedSceneIndex)
                EditorSceneManager.SetActiveScene(EditorSceneManager.GetSceneAt(activeSceneIndex));          ScenesInHierarchyView = new SceneSetup[] { };

            EditorApplication.update -= ReloadLastScene; ;
        }

        // Properties are remembered as editor preferences.
        private static string editorPrefLoadMasterOnPlay => "SAL." + PlayerSettings.productName + ".LoadMainSceneOnPlay";
        private static string editorPrefMasterScene => "SAL." + PlayerSettings.productName + ".MainScene";
        private static string editorPrefLoadedScenes => "SAL." + PlayerSettings.productName + ".LoadedScenes";

        public static bool LoadMainSceneOnPlay
        {
            get => EditorPrefs.GetBool(editorPrefLoadMasterOnPlay, false);
            set => EditorPrefs.SetBool(editorPrefLoadMasterOnPlay, value);
        }

        public static string MainScene
        {
            get => EditorPrefs.GetString(editorPrefMasterScene, String.Empty);
            set => EditorPrefs.SetString(editorPrefMasterScene, value);
        }

        public static SceneSetup[] ScenesInHierarchyView
        {
            get
            {
                string prefValue = EditorPrefs.GetString(editorPrefLoadedScenes, String.Empty);

                string[] tokens = prefValue.Split('|');
                int numScenes = tokens.Length / 3;

                SceneSetup[] scenes = new SceneSetup[numScenes];
                for (int i = 0; i < tokens.Length / 3; i++)
                {
                    scenes[i] = new SceneSetup
                    {
                        isActive = (tokens[i * 3 + 0] != "false"),
                        isLoaded = (tokens[i * 3 + 1] != "false"),
                        path = tokens[i * 3 + 2]
                    };
                }

                return scenes;
            }

            set
            {
                string prefValue = string.Join("|", value.Select(scene => (scene.isActive ? "true" : "false") + "|" + (scene.isLoaded ? "true" : "false") + "|" + scene.path).ToArray());
                EditorPrefs.SetString(editorPrefLoadedScenes, prefValue);
            }
        }
    }
}
