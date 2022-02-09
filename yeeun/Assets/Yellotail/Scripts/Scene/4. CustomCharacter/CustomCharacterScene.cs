using UnityEngine;
using Yellotail.CustomCharacter;

namespace Yellotail
{
    public class CustomCharacterScene : GameScene
    {
        [SerializeField] private CharacterViewUI characterViewUI;

        private void Awake()
        {
            characterViewUI.onSaved += GoNext;
        }
    }
}
