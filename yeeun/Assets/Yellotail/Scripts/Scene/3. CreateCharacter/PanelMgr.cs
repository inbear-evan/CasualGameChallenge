using UnityEngine;
using Yellotail.CreateCharacter;

namespace Yellotail
{
    public class PanelMgr : NickNamePanel
    {
        [SerializeField] CreatePanel createPanel;

        private void Awake()
        {
            createPanel.OnNextClicked += OnGoLeft;
        }
    }
}
