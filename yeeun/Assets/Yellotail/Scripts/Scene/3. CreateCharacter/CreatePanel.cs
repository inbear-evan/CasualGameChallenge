using System;
using UnityEngine;

namespace Yellotail.CreateCharacter
{
    public class CreatePanel : MonoBehaviour
    {
        [SerializeField] GameObject malePlayer;
        [SerializeField] GameObject femalePlayer;

        public event Action OnNextClicked;

        public void OnNext()
        {
            this.OnNextClicked?.Invoke();
        }
        public void OnToggledMale(bool isMale)
        {
            if (isMale)
            {
                malePlayer.SetActive(true);
                femalePlayer.SetActive(false);
                Debug.Log("Selected Male");
            }
            else
            {
                malePlayer.SetActive(false);
                femalePlayer.SetActive(true);
                Debug.Log("Selected Female");
            }
        }
    }
}
