using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Yellotail
{
    public class TitlePopUp : MonoBehaviour
    {
        [SerializeField] private GameObject roomPopUp;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TMP_InputField popUpInputField;
        [SerializeField] private Button okButton;


        public void OnClick()
        {
            roomPopUp.gameObject.SetActive(true);
        }

        public void OnOkButton()
        {
            inputField.text = popUpInputField.text;
            roomPopUp.gameObject.SetActive(false);
        }

        public void OnClickOutside()
        {
            roomPopUp.gameObject.SetActive(false);
            popUpInputField.text = inputField.text;
        }

    }
}
