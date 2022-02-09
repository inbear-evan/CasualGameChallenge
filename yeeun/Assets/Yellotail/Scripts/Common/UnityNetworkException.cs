using UnityEngine.Networking;

namespace Yellotail
{
    [System.Serializable]
    public class UnityNetworkException : System.Exception
    {
        public UnityWebRequest UnityWebRequest { get; }
        public long ResponseCode { get; }
        public string Method { get; }
        public string Url { get; }
        public override string Message { get; }

        public UnityNetworkException(UnityWebRequest uwr)
        {
            this.UnityWebRequest = uwr;
            this.ResponseCode = uwr.responseCode;
            this.Method = uwr.method;
            this.Url = uwr.url;
            this.Message = $"HTTP{ResponseCode} [{Method}] {Url}";
        }
    }
}
