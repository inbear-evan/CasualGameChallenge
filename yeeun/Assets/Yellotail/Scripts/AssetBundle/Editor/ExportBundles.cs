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

        [MenuItem("Yellotail Tools/¡Ú AssetBundle/Üâò¢?ê¹(F)Ù¼?ab?ê¹(T)")]
        public static void SimulateAssetBundle()
        {
            //ABMgr.SimulateAssetBundleInEditor = !ABMgr.SimulateAssetBundleInEditor;
            SimulateAssetBundleInEditor = !SimulateAssetBundleInEditor;
        }
        [MenuItem("Yellotail Tools/¡Ú AssetBundle/Üâò¢?ê¹(F)Ù¼?ab?ê¹(T)", true)]
        public static bool SimulateAssetBundleValidate()
        {
            Menu.SetChecked("Yellotail Tools/¡Ú AssetBundle/Üâò¢?ê¹(F)Ù¼?ab?ê¹(T)", SimulateAssetBundleInEditor);
            return true;
        }

        
        /// <summary>
        /// ?ð¶abøÐ?ê¹
        /// </summary>
        /// <returns></returns>
        [MenuItem("Yellotail Tools/¡Ú AssetBundle/?ð¶Ab?ê¹ÙþËì")]
        public static void ReBuildAllAssetBundles()
        {
            if (EditorApplication.isPlaying)
                return;

            string outputPath = outPut;
            Directory.Delete(outputPath, true);
            Debug.LogError("?ð¶ÙÍ?: " + outputPath);
        }
        /// <summary>
        /// ?ê¹øÐÙ¤Ù£
        /// </summary>
        /// <returns></returns>
        [MenuItem("Yellotail Tools/¡Ú AssetBundle/?ê¹øÐÙ¤Ù£")]
        public static void MakeAssetBundleNames()
        {
            if (EditorApplication.isPlaying)
                return;

            //?ð¶AbøÐ?ê¹Ù£
            string[] abNames = AssetDatabase.GetAllAssetBundleNames();
            for (int i = 0; i < abNames.Length; i++)
                AssetDatabase.RemoveAssetBundleName(abNames[i], true);
            Debug.LogError("?ð¶îïÝ»?ê¹AssetBundleÙ£?èÇà÷!");
            SetAssetsBundleName(resPath);
            Debug.LogError("?öÇAssetBundleÙ£?èÇà÷!");
        }

        [MenuItem("Yellotail Tools/¡Ú AssetBundle/Ab?ê¹öèøÐ")]
        public static void BuidldBundles()
        {
            if (EditorApplication.isPlaying)
                return;

            if (!Directory.Exists(resPath))
                Debug.LogError("?ê¹ÖØ?Üôðíî¤£º" + resPath);
            if (!Directory.Exists(outPut))
                Directory.CreateDirectory(outPut);
            if (!Directory.Exists(Application.streamingAssetsPath))
                Directory.CreateDirectory(Application.streamingAssetsPath);
            //÷ê?ÌÚÙþËìÍ¸???
            //CopyHotFix();

            //?ð¶AbøÐ?ê¹Ù£
            string[] abNames = AssetDatabase.GetAllAssetBundleNames();
            for (int i = 0; i < abNames.Length; i++)
                AssetDatabase.RemoveAssetBundleName(abNames[i], true);
            Debug.LogError("?ð¶îïÝ»?ê¹AssetBundleÙ£?èÇà÷!");
            SetAssetsBundleName(resPath);
            Debug.LogError("?öÇAssetBundleÙ£?èÇà÷!");
            //EditSpritAtlas.SetUIAtlas();
            BuildPipeline.BuildAssetBundles(outPut, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
            CreateAssetBundleFileInfo();
            AssetDatabase.Refresh();
            Debug.LogError("AssetBundleöèøÐèÇà÷!");

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
            Debug.LogError("?ê¹÷úÜâVersion:" + ver + "  ì«?ð¤Óðîòï·÷ù");
            Debug.LogError("ABFilesÙþËìßæà÷èÇà÷");
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
                    //ì¹ð¶ý¨?Û°øµÊ¥??ê¹ÜôéÄ?ÝÂý¨?
                    if (files[i].Name.Contains("."))
                        name = name.Remove(name.LastIndexOf("."));
                    //ôÕÊ¥ý¨?Û°øµ÷êÌÚãæ?ê¹ãáãÓ??ABfiles
                    assetImporter.assetBundleName = name + ".ubf";
                }
            }
        }
    }
}