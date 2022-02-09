using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Yellotail.AssetBundles
{
    public static class AssetBundleUtility
    {
        public const string AssetBundlesOutputDir = "AssetBundles";

        public static string GetPlatformName()
        {
#if UNITY_EDITOR
            return GetPlatformForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
#else
        return GetPlatformForAssetBundles(Application.platform);
#endif
        }

#if UNITY_EDITOR
        public static string GetPlatformForAssetBundles(BuildTarget target) => target switch
        {
            BuildTarget.Android => "Android",
            BuildTarget.iOS => "iOS",
            BuildTarget.tvOS => "tvOS",
            BuildTarget.WebGL => "WebGL",
            BuildTarget.StandaloneWindows => "StandaloneWindows",
            BuildTarget.StandaloneWindows64 => "StandaloneWindows64",
            BuildTarget.StandaloneOSX => "StandaloneOSX",

#if UNITY_SWITCH
            BuildTarget.Switch => "Switch",
#endif
            _ => target.ToString()
        };
#endif
        public static string GetPlatformForAssetBundles(RuntimePlatform platform) => platform switch
        {
            RuntimePlatform.Android => "Android",
            RuntimePlatform.IPhonePlayer => "iOS",
            RuntimePlatform.tvOS => "tvOS",
            RuntimePlatform.WebGLPlayer => "WebGL",
            RuntimePlatform.WindowsPlayer => "StandaloneWindows",
            RuntimePlatform.OSXPlayer => "StandaloneOSX",
#if UNITY_SWITCH
            RuntimePlatform.Switch => "Switch",
#endif
            _ => platform.ToString()
        };
    }    
}

