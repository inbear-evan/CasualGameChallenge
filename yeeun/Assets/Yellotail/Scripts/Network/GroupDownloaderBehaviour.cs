using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Yellotail
{
    // mono-behavior for downloading files, just set 'PendingURLS' in editor
    // invoke 'Download' or set 'DownloadOnStart' to true
    // some fields are provided for editor-proxy access to GroupDownloader object
    public class GroupDownloaderBehaviour : MonoBehaviour
    {
        [System.Serializable]
        public struct DownloadStruct
        {
            public string URI;
            public string FileName;
        }

        private GroupDownloader downloader;
        public GroupDownloader Downloader => downloader;

        // true if 'Download' should be invoked upon 'Start'
        [SerializeField] private bool downloadOnStart = true;
        [SerializeField] private string downloadPath;
        public string DownloadPath => downloadPath;

        // true if the download handler should complete on failure
        [SerializeField] private bool abandonOnFailure = false;

        [SerializeField] private List<string> pendingUrls = new List<string>();
        [SerializeField] private DownloadStruct[] uriToFileNames;
        [SerializeField] private string token = "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxNyIsImlhdCI6MTY0MDMyMDUyMSwiZXhwIjoxNjQwNzUyNTIxfQ.LDbRRw6NrJg2fxaFWvhdtM8RdWA1s0Gc_BrwaRwiLkE";

        public List<string> PendingUrls => pendingUrls;

        private void Start()
        {            
            //this.downloadPath = Application.persistentDataPath;
            if (!Directory.Exists(this.downloadPath))
            {
                Debug.LogError("EEEE");
            }
            
            this.downloader = new GroupDownloader(PendingUrls);
            this.downloader.Token = this.token;
            this.downloader.DownloadPath = this.downloadPath;
            this.downloader.AbandonOnFailure = this.abandonOnFailure;

            var URItoFileNameMap = new Dictionary<string, string>();
            foreach (var ds in uriToFileNames)
            {
                URItoFileNameMap.Add(ds.URI, ds.FileName);
            }

            this.downloader.URIFilenameMap = URItoFileNameMap;
            if (this.downloadOnStart && this.downloader != null)
            {
                this.downloader.Download();
            }
        }
    }
}
