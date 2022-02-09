using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public class InactivateGameObjectAfterTime : MonoBehaviour
    {
        [SerializeField] private float duration = 1.0f;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(this.duration);
        }
    }
}
