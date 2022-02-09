using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

using Yellotail.Crypto;

namespace Yellotail.AssetBundles
{    
    public class ExportBundles : MonoBehaviour
    {
        static string resPath = "Assets/BundleResources/";
        static string outPut = "../AssetBundles/Android/";

        static bool SimulateAssetBundleInEditor = false;

        [MenuItem("Yellotail Tools/�� AssetBundle/���?�(F)ټ?ab?�(T)")]
        public static void SimulateAssetBundle()
        {
            //ABMgr.SimulateAssetBundleInEditor = !ABMgr.SimulateAssetBundleInEditor;
            SimulateAssetBundleInEditor = !SimulateAssetBundleInEditor;
        }
        [MenuItem("Yellotail Tools/�� AssetBundle/���?�(F)ټ?ab?�(T)", true)]
        public static bool SimulateAssetBundleValidate()
        {
            Menu.SetChecked("Yellotail Tools/�� AssetBundle/���?�(F)ټ?ab?�(T)", SimulateAssetBundleInEditor);
            return true;
        }

        
        /// <summary>
        /// ?�ab��?�
        /// </summary>
        /// <returns></returns>
        [MenuItem("Yellotail Tools/�� AssetBundle/?�Ab?�����")]
        public static void ReBuildAllAssetBundles()
        {
            if (EditorApplication.isPlaying)
                return;

            string outputPath = outPut;
            Directory.Delete(outputPath, true);
            Debug.LogError("?���?: " + outputPath);
        }
        /// <summary>
        /// ?���٤٣
        /// </summary>
        /// <returns></returns>
        [MenuItem("Yellotail Tools/�� AssetBundle/?���٤٣")]
        public static void MakeAssetBundleNames()
        {
            if (EditorApplication.isPlaying)
                return;

            //?�Ab��?�٣
            string[] abNames = AssetDatabase.GetAllAssetBundleNames();
            for (int i = 0; i < abNames.Length; i++)
                AssetDatabase.RemoveAssetBundleName(abNames[i], true);
            Debug.LogError("?���ݻ?�AssetBundle٣?����!");
            SetAssetsBundleName(resPath);
            Debug.LogError("?��AssetBundle٣?����!");
        }

        [MenuItem("Yellotail Tools/�� AssetBundle/Ab?�����")]
        public static void BuidldBundles()
        {
            if (EditorApplication.isPlaying)
                return;

            if (!Directory.Exists(resPath))
                Debug.LogError("?���?�����" + resPath);
            if (!Directory.Exists(outPut))
                Directory.CreateDirectory(outPut);
            if (!Directory.Exists(Application.streamingAssetsPath))
                Directory.CreateDirectory(Application.streamingAssetsPath);
            //��?������͸???
            //CopyHotFix();

            //?�Ab��?�٣
            string[] abNames = AssetDatabase.GetAllAssetBundleNames();
            for (int i = 0; i < abNames.Length; i++)
                AssetDatabase.RemoveAssetBundleName(abNames[i], true);
            Debug.LogError("?���ݻ?�AssetBundle٣?����!");
            SetAssetsBundleName(resPath);
            Debug.LogError("?��AssetBundle٣?����!");
            //EditSpritAtlas.SetUIAtlas();
            BuildPipeline.BuildAssetBundles(outPut, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            CreateAssetBundleFileInfo();
            AssetDatabase.Refresh();
            Debug.LogError("AssetBundle��������!");

        }

        public static void CreateAssetBundleFileInfo()
        {
            string abRootPath = outPut;
            string abFilesPath = abRootPath + "/" + "abfiles.txt";
            if (File.Exists(abFilesPath))
                File.Delete(abFilesPath);

            var ExtName = ".ubf";
            var abFileList = new List<string>(Directory.GetFiles(abRootPath, "*" + ExtName, SearchOption.AllDirectories));
            abFileList.Add(abRootPath + AssetBundleUtility.GetPlatformName());
            FileStream fs = new FileStream(abFilesPath, FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2018, 1, 1));
            int ver = ((int)((DateTime.Now - startTime).TotalMinutes));
            sw.WriteLine(ver + "|" + DateTime.Now.ToString("u"));
            for (int i = 0; i < abFileList.Count; i++)
            {
                string file = abFileList[i];
                long size = 0;
                string md5 = CryptoUtility.GetMd5HashFromFile(file);
                string value = file.Replace(abRootPath, string.Empty).Replace("\\", "/");
                sw.WriteLine(value + "|" + md5 + "|" + size);
            }
            sw.Close();
            fs.Close();
            Debug.LogError("?�����Version:" + ver + "  �?��������");
            Debug.LogError("ABFiles������������");
        }

        static void SetAssetsBundleName(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileSystemInfo[] files = dir.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i] is DirectoryInfo)
                    SetAssetsBundleName(files[i].FullName);
                else
                {
                    if (files[i].FullName.EndsWith(".meta")) continue;
                    string assetPath = "Assets" + files[i].FullName.Substring(Application.dataPath.Length);
                    AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
                    //string name = assetPath.Substring(resPath.Length).Replace("\\", "/");
                    string name = assetPath.Substring(resPath.Length).Replace(@"\", "/");
                    //����?۰��ʥ??�����?����?
                    if (files[i].Name.Contains("."))
                        name = name.Remove(name.LastIndexOf("."));
                    //��ʥ��?۰��������?�����??ABfiles
                    assetImporter.assetBundleName = name + ".ubf";
                }
            }
        }
    }
}