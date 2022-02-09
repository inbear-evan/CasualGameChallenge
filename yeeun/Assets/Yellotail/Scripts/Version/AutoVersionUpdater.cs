using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif
using UnityEngine;

namespace Yellotail
{
    public class AutoVersionUpdater : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("Yellotail Tools/¡Ú Build/Version")]
        public static void IncrementVersion()
        {
            string version = PlayerSettings.bundleVersion;
            var parts = version.Split('.');
            var buildNumber = int.Parse(parts[parts.Length - 1]);

            ++buildNumber;
            parts[parts.Length - 1] = buildNumber.ToString();

            var newVersion = string.Join(".", parts);
            var newVersionCode = int.Parse(newVersion.Replace(".", ""));

            PlayerSettings.bundleVersion = newVersion;
            PlayerSettings.Android.bundleVersionCode = newVersionCode;
        }
        [PostProcessBuild(1080)]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            print("OnPostProcessBuild " + target + " " + path);
            IncrementVersion();
        }
#endif
    }
}
