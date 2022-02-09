using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public static class UnityTexture
    {
        private static Texture2D whiteTexture;
        private static Texture2D blackTexture;
        private static Texture2D transparentTexture;

        public static Texture2D WhiteTexture 
        {
            get
            {
                if (whiteTexture == null)
                {
                    whiteTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                    whiteTexture.SetPixel(0, 0, Color.white);
                    whiteTexture.Apply();
                }
                return whiteTexture;
            }
        }
        public static Texture2D BlackTexture
        {
            get
            {
                if (blackTexture == null)
                {
                    blackTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                    blackTexture.SetPixel(0, 0, Color.black);
                    blackTexture.Apply();
                }
                return blackTexture;
            }
        }
        public static Texture2D TransparentTexture
        {
            get
            {
                if (transparentTexture == null)
                {
                    transparentTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                    transparentTexture.SetPixel(0, 0, Color.clear);
                    transparentTexture.Apply();
                }
                return transparentTexture;
            }
        }
    }
}
