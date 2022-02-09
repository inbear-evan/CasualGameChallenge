using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// https://gist.github.com/ProGM/a40acd8ebbb91eb7b2295e65d5eb42c8
/// </summary>

namespace Yellotail
{
    [System.Serializable]
    public class MissingComponentInformation
    {
        private GameObject owner;        
        private List<(string, string)> componentNames;
        private List<(string, string)> animationClips;

        public GameObject Owner => this.owner;
        public List<(string, string)> ComponentNames => this.componentNames;
        public List<(string, string)> AnimationClips => this.animationClips;

        public MissingComponentInformation(GameObject owner)
        {
            this.owner = owner;
            this.componentNames = new List<(string, string)>();
            this.animationClips = new List<(string, string)>();
        }

        public void AddMissingComponent(string propertyName, string propertyPath)
        {
            this.componentNames.Add((propertyName, propertyPath));
        }
        public void AddMissingAnimationClip(string propertyName, string propertyPath)
        {
            this.animationClips.Add((propertyName, propertyPath));
        }
    }

    internal class MissingComponentFinder
    {
        public Dictionary<string, List<GameObject>> activeSceneObjectsDic;
        public Dictionary<GameObject, MissingComponentInformation> missingComponents;

        internal MissingComponentFinder()
        {
            activeSceneObjectsDic = new Dictionary<string, List<GameObject>>();
            this.missingComponents = new Dictionary<GameObject, MissingComponentInformation>();
        }

        internal bool IsFindComponents()
        {
            return activeSceneObjectsDic.Count > 0;
        }

        internal void FindMissingComponent()
        {
            var allObjects = Object.FindObjectsOfType<GameObject>();

            for (int i = 0; i < allObjects.Length; i++)
            {
                var sceneName = allObjects[i].scene.name;
                if (!activeSceneObjectsDic.ContainsKey(sceneName))
                    activeSceneObjectsDic.Add(sceneName, new List<GameObject>());

                var components = allObjects[i].GetComponents<Component>();
                for (int j = 0; j < components.Length; j++)
                {
                    if (components[j] == null)
                    {
                        activeSceneObjectsDic[sceneName].Add(allObjects[i]);
                        break;
                    }
                }
            }
        }

        internal void FindMissingComponent(string sceneName, GameObject[] gameObjects)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                var components = gameObjects[i].GetComponents<Component>();
                for (int comp = 0; comp < components.Length; comp++)
                {
                    var so = new SerializedObject(components[comp]);
                    var sp = so.GetIterator();

                    while (sp.NextVisible(true))
                    {
                        if (sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)
                        {
                            AddMissingComponent(gameObjects[i], sp.name, sp.propertyPath);
                        }
                    }

                    var animator = components[comp] as Animator;
                    if (animator != null)
                    {
                        FindMissingAnimationClips(gameObjects[i], animator);
                    }
                }
            }
        }

        internal void FindMissingAnimationClips(GameObject gameObject, Animator animator)
        {
            var animatorController = animator.runtimeAnimatorController;
            if (animatorController == null)
            {
                return;
            }

            for (int i = 0; i < animatorController.animationClips.Length; i++)
            {
                var so = new SerializedObject(animatorController.animationClips[i]);
                var sp = so.GetIterator();

                while (sp.NextVisible(true))
                {
                    if (sp.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        if (sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)
                        {
                            AddMissingAnimationClip(gameObject, sp.name, sp.propertyPath);
                        }
                    }
                }
            }
        }

        internal void AddMissingComponent(GameObject gameObject, string propertyName, string propertyPath)
        {
            if (!this.missingComponents.TryGetValue(gameObject, out var missingComponentInformation))
            {
                missingComponentInformation = new MissingComponentInformation(gameObject);
                this.missingComponents.Add(gameObject, missingComponentInformation);
            }

            missingComponentInformation.AddMissingComponent(propertyName, propertyPath);
        }
        internal void AddMissingAnimationClip(GameObject gameObject, string propertyName, string propertyPath)
        {
            if (!this.missingComponents.TryGetValue(gameObject, out var missingComponentInformation))
            {
                missingComponentInformation = new MissingComponentInformation(gameObject);
                this.missingComponents.Add(gameObject, missingComponentInformation);
            }

            missingComponentInformation.AddMissingAnimationClip(propertyName, propertyPath);
        }
        internal void DeleteMissingComponents()
        {
            var hasAnythingToDo = false;
            foreach (var keyVal in activeSceneObjectsDic)
            {
                var objs = keyVal.Value;
                for (int i = 0; i < objs.Count; i++)
                {
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(objs[i]);
                    Debug.Log("MissingComponentFinder: Removed missing component from " + objs[i].name);
                    hasAnythingToDo = true;
                }
            }
            if (!hasAnythingToDo)
                Debug.Log("MissingComponentFinder: Nothing to be removed!");
            activeSceneObjectsDic.Clear();
        }
    }
}
