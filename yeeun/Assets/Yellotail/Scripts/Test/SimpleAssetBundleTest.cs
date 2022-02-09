using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Yellotail
{
    public class SimpleAssetBundleTest : MonoBehaviour
    {
        [SerializeField] private string url;
        public string Url => url;
        
        [SerializeField] private string hash;
        public string Hash => hash;
        [SerializeField] private bool useHash = false;
        public bool UseHash => useHash && !string.IsNullOrEmpty(hash);

        [SerializeField] private Image progressBar;
        
        private IEnumerator Start()
        {
            this.progressBar.fillAmount = 0;

            var uwr = CreateUnityWebRequest();
            if (uwr == null)
                yield break;            

            yield return uwr.SendWebRequest();
            while (!uwr.isDone)
            {
                progressBar.fillAmount = uwr.downloadProgress;
                 yield return null;  
            }

            yield return null;
            var bundle = DownloadHandlerAssetBundle.GetContent(uwr);
            bundle.Unload(false);
        }        
        private UnityWebRequest CreateUnityWebRequest()
        {
            if (useHash)
            {
                return UnityWebRequestAssetBundle.GetAssetBundle(this.url, Hash128.Parse(this.hash));
            }
            else
            {
                return UnityWebRequestAssetBundle.GetAssetBundle(this.url);
            }            
        }

#if UNITY_EDITOR
        private static string assetBundleDirectory;
        public static string AssetBundleDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(assetBundleDirectory))
                {
                    var uri = new Uri(Path.Combine(Application.dataPath, "..", "AssetBundles"));
                    assetBundleDirectory = uri.ToString();
                }
                return assetBundleDirectory;
            }
        }

        [MenuItem("Yellotail Tools/¡Ú Build/AssetBundles")]        
        private static void BuildAssetBundles()
        {            
            FileUtility.CreateDirectoryIfDoesNotExist(AssetBundleDirectory);
            BuildPipeline.BuildAssetBundles(
                assetBundleDirectory, 
                BuildAssetBundleOptions.None, 
                BuildTarget.Android);
        }
#endif
    }
}
