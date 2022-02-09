using Nettention.Proud;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Yellotail
{
    [System.Serializable] public class ConnectHandler : UnityEvent<ErrorType> {}
    [System.Serializable] public class LoginHandler : UnityEvent<int> {}
    
    public partial class NetworkManager
        : SingletonBehaviour<NetworkManager>
    {
        [Header("[Network profile]")]
        [SerializeField] private NetworkProfile profile;
        private string guid => this.profile?.ServerGuid;
        private string ip => this.profile?.ServerIp;
        private int port => this.profile?.ServerPort ?? 0;

        private bool isConnected = false;
        public bool IsConnected
        {
            get => this.isConnected;
            private set => this.isConnected = value;
        }
        private NetClient client = new NetClient();
        public HostID HostID => client.LocalHostID;


        public ConnectHandler OnConnected;
        public LoginHandler OnLogined;

        private UserProtocol.Proxy userProxy;
        private UserProtocol.Stub userStub;

        public void ConnectToServer()
        {
            Debug.Log($"<color=cyan>ConnectToServer...</color>");
            var param = new NetConnectionParam
            {
                serverIP = this.ip,
                serverPort = (ushort)this.port,
                protocolVersion = new Nettention.Proud.Guid(this.guid),
                enableAutoConnectionRecovery = true,
            };
            this.client.JoinServerCompleteHandler = (ErrorInfo info, ByteArray server) =>
            {
                this.IsConnected = info.errorType == ErrorType.Ok;
                OnConnected?.Invoke(info.errorType);
            };
            this.client.LeaveServerHandler = (ErrorInfo info) =>
            {
                if (info.errorType == ErrorType.Ok)
                {
                    Debug.Log($"<color=red>Leave server</color>");
                }
                else
                {
                    Debug.LogError("Failed to leave server!");
                }
            };
            this.client.P2PMemberJoinHandler = (HostID id, HostID hostId, int count, ByteArray message) =>
            {

            };
            this.client.P2PMemberLeaveHandler = (HostID id, HostID hostId, int count) =>
            {

            };

            this.userProxy = new UserProtocol.Proxy();
            this.client.AttachProxy(this.userProxy);

            this.userStub = new UserProtocol.Stub();
            this.client.AttachStub(this.userStub);

            this.userStub.ResponseLogin = OnResponseLogin;

            bool result = this.client.Connect(param);
            Debug.Log(result);
        }

        public void Login(string id, string pw)
        {
            this.userProxy.RequestLogin(HostID.HostID_Server, RmiContext.ReliableSend, id, pw);
        }

        private bool OnResponseLogin(HostID remote, RmiContext rmiContext, int result)
        {
            this.OnLogined?.Invoke(result);
            return true;
        }

        private void Close()
        {
            this.client?.Dispose();
            this.client = null;
        }
        private void Update()
        {
            this.client?.FrameMove();
        }

        private void OnDestroy()
        {
            Close();
        }
    }
}
