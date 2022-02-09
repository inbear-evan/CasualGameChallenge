using agora_gaming_rtc;
using AgoraIO.Media;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Yellotail
{
    public class AgoraManager : MonoBehaviour
    {
        [SerializeField] private string appID;
        [SerializeField] private string appCertificate;
        [SerializeField] private string channelName;
        [SerializeField] private string logFile;
        [SerializeField] private uint uid = 2_882_341_273;
        [SerializeField] private uint ts = 86_400; // 24 * 60 * 60;
        [SerializeField] private uint salt = 1;
        public string accessToken;

#if UNITY_ANDROID
        private string[] permissionList = new []
        {
            Permission.Microphone,
            Permission.Camera,
        };
#endif

        private const float VIDEO_X_POS_OFFSET = 20.0f;

        private bool isInitialized;
        public bool IsInitialized => this.isInitialized;

        private string callId;
        private IRtcEngine rtcEngine;
        [SerializeField] private VideoSurface myView;
        [SerializeField] private VideoSurface remoteView;
        private List<VideoSurface> remoteViews = new List<VideoSurface>();

        [SerializeField] private Button joinButton;
        [SerializeField] private Button leaveButton;

        private void Start()
        {
            InitUI();

        
            GenerateAccessToken();
            InitAgoraEngine();
        }

        private void InitUI()
        {
            this.joinButton.interactable = true;
            this.joinButton.onClick.AddListener(JoinChannel);

            this.leaveButton.interactable = false;
            this.leaveButton.onClick.AddListener(LeaveChannel);

            var vss = FindObjectsOfType<VideoSurface>();
            for (int i = 0; i < vss.Length; i++)
            {
                vss[i].SetEnable(false);
            }
        }

        private void Update()
        {
            CheckPermissions();
        }

        private void GenerateAccessToken()
        {
            var token = new AccessToken(
                this.appID,
                this.appCertificate,
                this.channelName,
                this.uid.ToString(),
                this.ts,
                this.salt);
            uint expiredTs = (uint)GameManager.UnixTimeStamp + ts;
            token.addPrivilege(Privileges.kPublishVideoStream, expiredTs);

            this.accessToken = token.build();
        }

        private void OnUserJoined(uint uid, int elapsed)
        {
            Debug.Log($"OnUserJoined: {uid}");

            this.remoteView.SetForUser(uid);
            this.remoteView.SetEnable(true);
            this.remoteView.SetVideoSurfaceType(AgoraVideoSurfaceType.Renderer);
            //this.remoteView.SetVideoSurfaceType(AgoraVideoSurfaceType.RawImage);
        }

        private void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
        {
            this.remoteView.SetEnable(false);
        }

        private void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
        {
            this.callId = this.rtcEngine.GetCallId();
            Debug.Log($"<color=cyan>Call ID: {this.callId} - {uid}</color>");

            this.myView.SetEnable(true);
            this.myView.SetForUser(0);
            this.myView.SetVideoSurfaceType(AgoraVideoSurfaceType.Renderer);
        }

        private void OnLeaveChannel(RtcStats stats)
        {
            this.myView.SetEnable(false);
            this.remoteView.SetEnable(false);

            Debug.Log($"<color=cyan>Sending Rating: {this.rtcEngine.Rate(callId, 4, "Rating")}</color>");
        }

        private void JoinChannel()
        {
            this.rtcEngine.EnableVideo();
            this.rtcEngine.EnableVideoObserver();
            this.myView.SetEnable(true);
            this.rtcEngine.JoinChannelByKey(this.accessToken, this.channelName, "", this.uid);

            //this.rtcEngine.JoinChannelByKey(
            //    channelKey:"0062d2be395468c441f8f1eec7c88efbbcfIAB5WL++DteOJT9xzj3sYYiiQbj+ncd7JPdMIfDD/3tpPf2gtqAAAAAAIgDC8VeAD7/nYQQAAQAPv+dhAgAPv+dhAwAPv+dhBAAPv+dh",
            //    channelName:"agora",
            //    uid: this.uid);

            Debug.Log($"Join channel");
        }

        private void LeaveChannel()
        {
            if (!this.IsInitialized)
            {
                Debug.LogError($"WebRTCEngine is not initialized!");
                return;
            }

            this.rtcEngine.LeaveChannel();
            this.rtcEngine.DisableVideo();
            this.rtcEngine.DisableVideoObserver();
        }

        private void CreateUserVideoSurface(uint uid, bool localUser)
        {
            if (ExistsUser(uid))
            {
                Debug.LogWarning($"<color=yellow>Attempting to duplicate videosurface for user: {uid}</color>");
                return;
            }

            var go = GameObject.CreatePrimitive(PrimitiveType.Plane);
            go.name = uid.ToString();
            go.transform.Rotate(-90.0f, 0.0f, 0.0f);

            var x = Random.Range(-VIDEO_X_POS_OFFSET, VIDEO_X_POS_OFFSET);
            go.transform.position = new Vector3(x, 5, 10.0f);

            var view = go.AddComponent<VideoSurface>();
            this.remoteViews.Add(view);

            if (!localUser)
            {
                view.SetForUser(uid);
            }
        }

        private void RemoveUserVideoSurface(uint uid)
        {
            for (int i = 0; i < this.remoteViews.Count; ++i)
            {
                if (this.remoteViews[i].name == uid.ToString())
                {
                    var view = this.remoteViews[i];
                    GameObject.Destroy(view.gameObject);

                    this.remoteViews.RemoveAt(i);
                    break;
                }
            }
        }

        private bool ExistsUser(uint uid)
        {
            for (int i = 0; i < this.remoteViews.Count; ++i)
            {
                if (this.remoteViews[i].name == uid.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        private void CheckPermissions()
        {
#if UNITY_ANDROID
            foreach (var permission in this.permissionList)
            {
                if (!Permission.HasUserAuthorizedPermission(permission))
                {
                    Permission.RequestUserPermission(permission);
                }
            }
#endif
        }

        private void InitAgoraEngine()
        {
            //DisposeAgoraEngine();

            this.rtcEngine = IRtcEngine.GetEngine(this.appID);
            //this.rtcEngine = IRtcEngine.GetEngine("2d2be395468c441f8f1eec7c88efbbcf");
            if (!string.IsNullOrEmpty(this.logFile))
                this.rtcEngine.SetLogFile(this.logFile);

            this.rtcEngine.SetChannelProfile(CHANNEL_PROFILE.CHANNEL_PROFILE_LIVE_BROADCASTING);
            this.rtcEngine.SetClientRole(CLIENT_ROLE_TYPE.CLIENT_ROLE_BROADCASTER);
            this.rtcEngine.OnUserJoined += OnUserJoined;
            this.rtcEngine.OnUserOffline += OnUserOffline;
            this.rtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess;
            this.rtcEngine.OnLeaveChannel += OnLeaveChannel;
            this.rtcEngine.OnWarning = OnWarning;
            this.rtcEngine.OnError = OnError;
            //this.rtcEngine.OnTokenPrivilegeWillExpire = OnTokenPrivilegeWillExpire;

            this.isInitialized = true;
        }

        private void OnError(int error, string msg)
        {
            Debug.LogError($"<color=red>{error}: {msg}</color>");
        }

        private void OnWarning(int warn, string msg)
        {
            Debug.LogWarning($"<color=yellow>{warn}: {msg}</color>");
        }

        private void DisposeAgoraEngine()
        {
            if (this.rtcEngine != null)
            {
                this.rtcEngine.LeaveChannel();
                this.rtcEngine = null;
                this.isInitialized = false;

                IRtcEngine.Destroy();
            }
        }

        private void OnApplicationQuit()
        {
            DisposeAgoraEngine();
        }
    }
}
