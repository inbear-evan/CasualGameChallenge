using System.Collections.Generic;
using UnityEngine;
using Yellotail.UI.Dialog;

namespace Yellotail.Lobby
{
    public class RoomPageItem : MonoBehaviour
    {
        [SerializeField] private List<RoomItem> rooms;
        private const int pageRooms = 5;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void UpdateView(RoomPageModel model)
        {
            for(int i = 0; i < model.rooms.Count; i++)
            {
                rooms[i].UpdateView(model.rooms[i]);
            }

            for(int i = model.rooms.Count; i < pageRooms; i++)
            {
                rooms[i].UpdateView(null);
            }
        }

        public void OnDisableView()
        {

        }
    }
}
