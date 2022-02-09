using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail.InGame
{
    public abstract class CameraInput : MonoBehaviour
    {
        public abstract float GetHorizontalCameraInput();
        public abstract float GetVerticalCameraInput();
        public abstract float GetZoomCameraInput();
    }
}
