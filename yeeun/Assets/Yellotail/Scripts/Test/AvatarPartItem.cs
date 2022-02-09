using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Yellotail
{
//  [ExecuteInEditMode]
    public class AvatarPartItem : MonoBehaviour
    {
        [SerializeField] private AvatarPart partDef;
        [SerializeField] private Text partText;
        [SerializeField] private AvatarSystem avatarSystem;

        private Toggle[] toggles;

        private void Awake()
        {
            //var partName = partDef.ToString();
            //gameObject.name = gameObject.name + "_" + partName;
            //partText.text = partName;

            this.toggles = GetComponentsInChildren<Toggle>();
        }

        public void ChangePart(int id, GameObject part)
        {
            avatarSystem.ChangeAvatarPart(partDef, id, part);
        }
        public void RandomPart()
        {
            var toggle = toggles.RandomElement();
            if (!toggle.isOn)
            {
                toggle.isOn = true;
            }
            else
            {
                var def = toggle.GetComponent<AvatarPartDef>();
                ChangePart(def.ID, def.Part);
            }
        }
    }
}
