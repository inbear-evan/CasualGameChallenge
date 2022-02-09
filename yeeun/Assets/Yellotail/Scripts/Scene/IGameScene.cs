using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    internal interface IGameScene
    {
        string Name { get; }
        void OnEnter();
        void OnLeave();        
    }
}
