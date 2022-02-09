using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail.CustomCharacter
{
    public class CharacterViewUI : MonoBehaviour
    {
        public event Action onSaved;

        public void OnSaveButton()
        {
            onSaved?.Invoke();
        }
    }
}
