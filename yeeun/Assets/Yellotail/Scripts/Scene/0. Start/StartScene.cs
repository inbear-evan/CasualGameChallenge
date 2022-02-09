using System.Collections;
using UnityEngine;

namespace Yellotail
{
    public class StartScene : GameScene
    {
        [SerializeField] private float waitTime = 1.0f;
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(this.waitTime);
            GoNext();
        }
    }
}