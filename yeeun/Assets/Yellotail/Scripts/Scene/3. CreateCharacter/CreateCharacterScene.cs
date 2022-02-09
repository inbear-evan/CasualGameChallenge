using UnityEngine;
using Yellotail.CreateCharacter;


namespace Yellotail
{
    public class CreateCharacterScene : GameScene
    {
        [SerializeField] CreatePanel createPanel;
        [SerializeField] NickNamePanel nickNamePanel;

        private void Awake()
        {
            createPanel.OnNextClicked += nickNamePanel.Open;
            nickNamePanel.OnNextClicked += GoNext;
        }
    }
}
