using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

namespace Yellotail
{
    public class AvatarSystem : MonoBehaviour
    {
        private Animator animator;

        [SerializeField] private string rootBoneName = "Bip001";

        private Transform rootBone;
        private Transform[] bones;
        private Dictionary<string, Transform> boneTable = new Dictionary<string, Transform>();

        private Dictionary<AvatarPart, AvatarPartDefinition> avatarParts = new Dictionary<AvatarPart, AvatarPartDefinition>();

        private void Awake()
        {
            this.animator = GetComponent<Animator>();

            InitBones();
        }

        private void InitBones()
        {
            if (this.rootBone != null && this.bones != null)
            {
                Debug.Log("!@!!");
                return;
            }                

            this.rootBone = transform.Find(rootBoneName);
            if (this.rootBone != null)
            {
                this.bones = this.rootBone.GetComponentsInChildren<Transform>();
                this.boneTable = this.bones.ToDictionary(bone => bone.name);
            }

            var smrs = GetComponentsInChildren<SkinnedMeshRenderer>(true);
            for (int i = 0; i < smrs.Length; i++)
            {
                // 삭제하면 파츠 교체시 애니메이션이 작동 안한다.
                // 일단 비활성화한 후 ....
                //GameObject.Destroy(smrs[i].gameObject);
            }
        }

        public void ChangeAvatarPart(AvatarPart avatarPart, int id, GameObject prefabGameObject)
        {
            Debug.Log($"<color=lime>{avatarPart} : {id}</color>");
            if (!this.avatarParts.TryGetValue(avatarPart, out var part))
            {
                part = new AvatarPartDefinition
                {
                    part = avatarPart,
                    prefab = prefabGameObject,
                    partInstance = null
                };
                this.avatarParts[avatarPart] = part;
            }

            var oldPart = part.partInstance?.gameObject;

            var partGameObject = Instantiate(prefabGameObject);
            partGameObject.transform.SetParent(transform, false);

            part.partInstance = partGameObject.transform;

            SetupBones(partGameObject);

            if (oldPart != null)
            {
                GameObject.Destroy(oldPart);
            }

            this.animator.Rebind();
        }
       
        private void SetupBones(GameObject avatarPart)
        {
            var rnds = avatarPart.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var smr in rnds)
            {
                var listBones = new List<Transform>();
                foreach (var bone in smr.bones)
                {
                    if (this.boneTable.TryGetValue(bone.name, out var boneTrans))
                    {
                        listBones.Add(boneTrans);
                    }
                    else
                    {
                        Debug.LogError($"<color=red>Can't find bone: {bone.name}</color>");
                    }
                }

                smr.bones = listBones.ToArray();
                if (smr.rootBone != null)
                {
                    smr.rootBone = this.boneTable[smr.rootBone.name];
                }
                else
                {
                    Debug.LogError("Root bone is null!");
                }
            }

            var rootBone = avatarPart.transform.Find(this.rootBoneName);
            if (rootBone != null)
            {
                GameObject.Destroy(rootBone.gameObject);
            }
        }
    }
}

