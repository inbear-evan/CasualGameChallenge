using UnityEngine;
using UnityEngine.EventSystems;

namespace Yellotail
{
    public class CharacterRotation : MonoBehaviour, IDragHandler
    {
        [SerializeField] private Transform characterPivot;
        [SerializeField] private float rotationSpeed = 0.4f;

        public void OnDrag(PointerEventData eventData)
        {
            characterPivot.Rotate(0, -eventData.delta.x * rotationSpeed, 0);
        }

    }
}