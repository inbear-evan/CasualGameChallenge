using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Yellotail
{
    public abstract class GameScene : MonoBehaviour
    {
        [SerializeField] protected SceneUniqueID sceneID;
        [Header("[Scene Transition]")]        
        [SerializeField] private SceneUniqueID prevSceneID;
        [SerializeField] private SceneUniqueID nextSceneID;       

        public SceneUniqueID SceneID => this.sceneID;

        protected void GoBack()
        {
            ChangeScene(this.prevSceneID);
        }
        protected void GoNext()
        {
            ChangeScene(this.nextSceneID);
        }

        private void ChangeScene(SceneUniqueID sceneID)
        {
            if (sceneID != SceneUniqueID.None)
                SceneManager.LoadSceneAsync((int)sceneID, LoadSceneMode.Single);
        }
    }
}
