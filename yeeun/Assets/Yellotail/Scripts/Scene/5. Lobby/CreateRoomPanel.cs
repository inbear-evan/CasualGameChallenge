using TMPro;
using UnityEngine;

namespace Yellotail.Lobby
{
    public class CreateRoomPanel : MonoBehaviour
    {
        [SerializeField] RoomTypeList roomTypeList;
        [SerializeField] private TMP_InputField roomTitle;
        [SerializeField] private TMP_Text roomNumber;


        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void OnClose()
        {
            gameObject.SetActive(false);
        }
        public int roomNumberValue = 4;
        public int roomNumberMin = 2;
        public int roomNumberMax = 8;


        public void OnMinusButtonClick()
        {
            if (roomNumberValue > roomNumberMin)
            {
                roomNumberValue--;
                roomNumber.text = roomNumberValue.ToString();
            }
        }

        public void OnPlusButtonClick()
        {
            if (roomNumberValue < roomNumberMax)
            {
                roomNumberValue++;
                roomNumber.text = roomNumberValue.ToString();
            }
        }
        public void OnClick()
        {
            Debug.Log("방 제목 : " + roomTitle.text);
            Debug.Log("방 인원수 : " + roomNumber.text);
            Debug.Log("방 종류 : " + roomTypeList.SelectedView.Index);
            GameManager.Instance.LoadLevel(roomTypeList.SelectedView.Index);
        }
    }
}
