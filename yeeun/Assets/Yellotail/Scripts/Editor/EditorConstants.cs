using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Yellotail
{
    public static class EditorConstants
    {
        private static readonly string RootPath = "Assets/Yellotail";
        private static readonly string UIPath = $"{RootPath}/Textures/UI/";
        private static readonly string CharacterModelPath = $"{RootPath}/Models/Characters/";
        private static readonly string CharacterAnimationPath = $"{RootPath}/Models/Animations/";
        private static readonly string[] NormalMapSuffix = { "_N", "_Normal" };
        

        public static bool IsSprite(string path)
        {
            return path.StartsWith(EditorConstants.UIPath, StringComparison.OrdinalIgnoreCase);
        }
        public static bool IsCharacterModel(string path)
        {
            return path.StartsWith(EditorConstants.CharacterModelPath, StringComparison.OrdinalIgnoreCase);
        }
        public static bool IsCharacterAnimation(string path)
        {
            return path.StartsWith(EditorConstants.CharacterAnimationPath, StringComparison.OrdinalIgnoreCase);
        }
        public static bool IsNormalMap(string path)
        {
            var filename = Path.GetFileNameWithoutExtension(path);
            for (int i = 0; i < EditorConstants.NormalMapSuffix.Length; i++)
            {
                if (filename.EndsWith(EditorConstants.NormalMapSuffix[i], StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
