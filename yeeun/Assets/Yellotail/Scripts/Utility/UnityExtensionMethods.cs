using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public static partial class ExtensionMethods
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }

        public static bool IsNullOrEmpty(ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static void SafeDestroyChildren(GameObject gameObject)
        {
            SafeDestroyChildren(gameObject.transform);
        }
        public static void SafeDestroyChildren(Transform transform)
        {
            //for (int i = 0; i < transform.childCount; i++)
            foreach (Transform child in transform)
            {
                //SafeDestroy(transform.GetChild(i).gameObject);
                SafeDestroy(child.gameObject);
            }
        }
        public static void ParentAndFillRectTransform(Transform child, Transform parent)
        {
            var tableTrans = child.transform as RectTransform;
            tableTrans.SetParent(parent, false);
            tableTrans.anchorMin = Vector2.zero;
            tableTrans.anchorMax = Vector2.one;
            tableTrans.offsetMin = tableTrans.offsetMax = Vector2.zero;
        }
        public static void SafeDestroy(GameObject gameObject)
        {
            if (gameObject != null)
            {
#if UNITY_EDITOR
                if (Application.isPlaying)
                {
                    UnityEngine.Object.Destroy(gameObject);
                }
                else
                {
                    UnityEditor.EditorApplication.delayCall += () => UnityEngine.Object.DestroyImmediate(gameObject);
                }
#else
                UnityEngine.Object.Destroy(gameObject);
#endif
            }
        }

        public static Transform FindParent(this Transform transform, string name)
        {
            var current = transform;
            while (current.parent != null)
            {
                current = current.parent;
                if (name.Equals(current.name))
                    return current;
            }
            return null;
        }
        public static Transform FindSibling(this Transform transform, string name)
        {
            if (transform.parent)
                return transform.parent.Find(name);

            return null;
        }
        public static Transform FindFromRoot(this Transform transform, string name)
        {
            return transform.root.Find(name);
        }

        public static void SnapTo(this Transform self, Transform target)
        {
            self.position = target.position;
            self.rotation = target.rotation;
        }
        public static void SnapTo(this Transform self, GameObject target)
        {
            self.position = target.transform.position;
            self.rotation = target.transform.rotation;
        }
        public static void SnapAndParent(this Transform self, Transform parent)
        {
            self.parent = parent;
            self.localPosition = Vector3.zero;
            self.localRotation = Quaternion.identity;
        }
        public static void ResetPosition(this Transform transform)
        {
            transform.position = Vector3.zero;
        }
        public static void ResetRotation(this Transform transform)
        {
            transform.rotation = Quaternion.identity;
        }
        public static void ResetScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }
        public static void ResetTransform(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
            return component;
        }
        public static T GetOrAddComponent<T>(this Component component) where T : Component => component.gameObject.GetOrAddComponent<T>();

        public static string GetHierachyPath(this GameObject gameObject)
        {
            return gameObject.transform.GetHierachyPath();
        }
        public static string GetHierachyPath(this Transform transform)
        {
            return transform.parent == null ? transform.name : GetHierachyPath(transform.parent) + "/" + transform.name;
        }
    }
}
