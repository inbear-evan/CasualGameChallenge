using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public class RotateIt : MonoBehaviour
    {
        [SerializeField] private float speed = 90.0f;

        void Update()
        {
            transform.Rotate(0, this.speed * Time.deltaTime, 0);
        }
    }
}
