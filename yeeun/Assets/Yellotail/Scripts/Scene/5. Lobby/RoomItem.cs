using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yellotail.UI.Dialog;
using TMPro;
using Yellotail.Lobby;

namespace Yellotail
{
    public class RoomItem : MonoBehaviour
    {
        [SerializeField] private Image thumnail;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text master;
        [SerializeField] RoomPageList roomPageList;
        private int roomindex;
        


        public void UpdateView(RoomInfo info)
        {
            if (info == null)
            {
                gameObject.SetActive(false);
                return;
            }

            title.text = info.title;
            master.text = info.master;
            roomindex = info.index;
        }

        public void OnEnterButton()
        {
           GameManager.Instance.LoadLevel(roomindex);
        }
    }
}
