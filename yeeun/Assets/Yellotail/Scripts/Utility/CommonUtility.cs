using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

using Random = UnityEngine.Random;

using Object = UnityEngine.Object;
using Newtonsoft.Json.Linq;

namespace Yellotail
{
    public static class CommonUtility
    {
        public static string ToHexString(this byte[] data)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < data.Length; ++i)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static T RandomElement<T>(this T[] array)
        {
            int index = Random.Range(0, array.Length);
            return array[index];
        }

        public static string ToColorText(Color color, string text) => $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>";        
    }
}
