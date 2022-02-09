using TMPro;
using UnityEngine;

namespace Yellotail
{
    public class UserName : MonoBehaviour
    {
        [SerializeField] private TMP_Text userName;

        private void Start()
        {
            userName.text = GameManager.Instance.GameData.User.Name;
        }
    }
}
