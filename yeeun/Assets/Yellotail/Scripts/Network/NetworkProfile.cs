using UnityEngine;

namespace Yellotail
{
    [CreateAssetMenu(fileName = "NetworkProfile", menuName = "¡Ú Yellotail/Network/Create Profile")]
    public class NetworkProfile : ScriptableObject
    {
        [SerializeField] private string serverGuid;
        [SerializeField] private string serverIp;
        [SerializeField] private int serverPort;

        public string ServerGuid => this.serverGuid;
        public string ServerIp => this.serverIp;
        public int ServerPort => this.serverPort;
    }
}
