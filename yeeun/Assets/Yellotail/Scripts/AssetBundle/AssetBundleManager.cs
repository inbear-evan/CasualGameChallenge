using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Yellotail;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
using UnityEditor;
#endif

namespace Yellotail.AssetBundles
{
    public class AssetBundleManager : SingletonBehaviour<AssetBundleManager>
    {
        public static Dictionary<string, string> assetBundleUrl = new Dictionary<string, string>();
        public static AssetBundleManifest assetBundleManifest = null;

        private static string[] activeVariants = { };

        private static Dictionary<string, string[]> dependencies = new Dictionary<string, string[]>();
    //    public static Dictionary<string, LoadAssetBundle> loadedAssetBundles = new Dictionary<string, LoadAssetBundle>();

#if UNITY_EDITOR
        public static string simulateAssetBundlesKey = "U$7j7H=i40kF1j+KWJ2M";
        public static bool SimulateAssetBundleInEditor
        {
            get => EditorPrefs.GetBool(simulateAssetBundlesKey, false);
            set => EditorPrefs.SetBool(simulateAssetBundlesKey, value);
        }
#endif

        public void Initialize()
        {
#if UNITY_EDITOR
            if (!SimulateAssetBundleInEditor)
                return;            
#endif

            string platformName = AssetBundleUtility.GetPlatformName();
            if (!assetBundleUrl.TryGetValue(platformName, out var url))
            {
          //      url = GetRelativePath() + platformName;
            }
            Debug.Log($"AssetBundles Url: {url}");

            var manifest = AssetBundle.LoadFromFile(url);
        }

    }
}
