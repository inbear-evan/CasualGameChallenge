using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public class RandomPart : MonoBehaviour
    {
        private AvatarPartItem[] items;

        private void Start()
        {
            items = transform.parent.GetComponentsInChildren<AvatarPartItem>();
            OnClick();
        }

        public void OnClick()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].RandomPart();
            }
        }
    }
}
