using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
