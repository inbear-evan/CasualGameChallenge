using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Yellotail
{
    public class AvatarPartDef : MonoBehaviour
    {
        [SerializeField] private int id;
        public int ID => id;

        [SerializeField] private GameObject part;
        public GameObject Part => part;

        private Toggle toggle;
        public Toggle Toggle => toggle;

        [SerializeField] private AvatarPartItem avatarPartItem;

        private void Awake()
        {
            this.toggle = GetComponent<Toggle>();
            this.toggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    avatarPartItem.ChangePart(id, part);
                }
            });
        }
    }
}
