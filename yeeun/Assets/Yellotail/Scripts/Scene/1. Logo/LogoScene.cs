using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public class LogoScene : GameScene
    {
        [SerializeField] private float waitTime = 1.0f;
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(this.waitTime);
            GoNext();
        }
    }
}
