/* UltimateJoystickScreenSizeUpdater.cs */
/* Written by Kaz Crowe */

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Yellotail
{
    public class UltimateJoystickScreenSizeUpdater : UIBehaviour
    {
        protected override void OnRectTransformDimensionsChange()
        {
            if (gameObject == null || !gameObject.activeInHierarchy)
                return;

            StartCoroutine("YieldPositioning");
        }

        private IEnumerator YieldPositioning()
        {
            yield return new WaitForEndOfFrame();

            UltimateJoystick[] allJoysticks = FindObjectsOfType(typeof(UltimateJoystick)) as UltimateJoystick[];

            for (int i = 0; i < allJoysticks.Length; i++)
                allJoysticks[i].UpdatePositioning();
        }
    }
}
