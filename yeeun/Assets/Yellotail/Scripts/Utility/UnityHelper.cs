using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Yellotail
{
    public static class UnityHelper
    {
        public static bool IsPointerOverGameObject()
        {
            return IsPointerOverGameObject(Input.mousePosition);
        }

        public static bool IsPointerOverGameObject(Vector2 mousePosition)
        {
            var ped = new PointerEventData(EventSystem.current);
            ped.pressPosition = mousePosition;
            ped.position = mousePosition;

            var list = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, list);
            return list.Count > 0;
        }

        public static Vector3 WorldPosToUiPos(Vector3 worldPos, Canvas uiCanvas)
        {
            var pt = Camera.main.WorldToScreenPoint(worldPos);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(uiCanvas.transform as RectTransform, pt, uiCanvas.worldCamera, out var v);
            return v;
        }
    }
}
