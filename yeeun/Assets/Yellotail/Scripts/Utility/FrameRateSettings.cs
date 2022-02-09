using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public class FrameRateSettings : MonoBehaviour
    {
        [SerializeField] private int targetFrameRate;
        private void Start()
        {
            if (Application.targetFrameRate != this.targetFrameRate)
            {
                Application.targetFrameRate = this.targetFrameRate;
            }
        }
    }
}
