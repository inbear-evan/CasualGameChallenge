using TMPro;
using UnityEngine;

namespace Yellotail
{
    public class MessageText : MonoBehaviour
    {
        [SerializeField] private TMP_Text userName;
        private void Start()
        {
            userName.text = GameManager.Instance.GameData.User.Description;
        }
    }
}