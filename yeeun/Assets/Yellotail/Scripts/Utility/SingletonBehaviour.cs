using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public class SingletonBehaviour<T> : 
        MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance => GetInstance();

        private static object sync = new object();
        private static bool isAppQuit = false;

        private static T GetInstance()
        {
            if (isAppQuit)
                return null;

            lock (sync)
            {
                if (instance == null)
                {
                    var singletonName = typeof(T).Name;

                    T[] singletons = FindObjectsOfType<T>();
                    if (singletons.Length > 0)
                    {
                        instance = singletons[0];
                    }

                    if (singletons.Length > 1)
                    {
                        Debug.LogWarning($"{singletonName} is not unique! you should fix it.");                        
                    }

                    if (instance == null)
                    {
                        var gameObject = new GameObject(singletonName + " - Signleton");
                        instance = gameObject.AddComponent<T>();
                    }
                }

                return instance;
            }            
        }

        protected virtual void OnApplicationQuit()
        {
            isAppQuit = true;
        }
    }
}
