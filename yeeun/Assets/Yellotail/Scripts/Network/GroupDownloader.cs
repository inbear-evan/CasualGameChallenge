// https://github.com/isaveu/UnityGroupDownloader/blob/main/GroupDownloader.cs
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Yellotail
{
    // downloads multiple files to a path ( one-after-another execution )
    // events for download failure and success
    // all pending URI's should be pre-checked for syntax
    // can assign filenames via filenameToURI or 
    public class GroupDownloader
    {
        // path to download each file appended by the file name ofc
        // defaults to persistent data path
        private string downloadPath = Application.persistentDataPath;

        public string DownloadPath
        {
            get =>downloadPath;
            set => downloadPath = value;
        }

        // true if any files are being downloaded currently
        private bool downloading = false;
        public bool Downloading => downloading;
        

        // true if the download handler should continue on failure
        private bool abandonOnFailure = true;
        public bool AbandonOnFailure
        {
            get => abandonOnFailure;            
            set => abandonOnFailure = value;
        }

        // time before timing out a specific download
        public int Timeout = 10;

        public delegate void GroupDownloadFailureEvent(bool completed, string uri, string fileResultPath);
        public delegate void GroupDownloadSuccessEvent(bool completed, string uri, string fileResultPath);

        // triggered when a download fails
        // may be invoked multiple times
        public event GroupDownloadFailureEvent OnDownloadFailure;

        // triggered when the entire group succeeds
        public event GroupDownloadSuccessEvent OnDownloadSuccess;

        // option for delivering filenames from URI
        public delegate string URIToFilename(string uri);

        // true to get filenames from this map
        // otherwise use URIToFilename delegate
        private bool useUriFilenameMap = true;
        public bool UseURIFilenameMap
        {
            get => useUriFilenameMap;
            set => useUriFilenameMap = value;
        }

        private URIToFilename onUriToFilename;
        public URIToFilename OnURIToFilename
        {
            get => onUriToFilename;
            set => onUriToFilename = value;
        }

        public Dictionary<string, string> uriFilenameMap = new Dictionary<string, string>();
        public Dictionary<string, string> URIFilenameMap
        {
            get => uriFilenameMap;
            set => uriFilenameMap = value;
        }

        // urls to download in increasing index order
        // urls are removed after completion or failure
        private List<string> pendingUrls = new List<string>();

        // urls that downloaded succesfully
        // do not manually load or remove items
        private List<string> completedUrls = new List<string>();

        // urls that were abandoned and not downloaded
        // do not manually load or remove items
        private List<string> uncompletedUrls = new List<string>();

        // populated with succesfully downloaded 
        private List<string> completedURLPaths = new List<string>();

        public List<string> PendingURLS => pendingUrls;
        public List<string> CompletedURLS => completedUrls;
        public List<string> UncompletedURLS => uncompletedUrls;
        public List<string> CompletedURLPaths => completedURLPaths;

        // download progress of all succesful and pending downloads
        /// from 0f to 1f 
        private float progress = 0f;

        // number of qued files calculated when 'Download' is invoked
        private int initialCount;

        private string token;
        public string Token
        {
            get => this.token;
            set => this.token = value;
        }

        public float Progress => progress;

        // true if the download expierenced an error or got cancelled
        public bool DidError => UncompletedURLS.Count > 0;

        // true if the download is finished and not in progress
        public bool DidFinish => PendingURLS.Count == 0;

        public int startTime = -1;
        public int endTime = -1;
        public int _elapsedTime = -1;

        // time the last download was initiated at
        public int StartTime => startTime;

        // time the last download ended, cancelled or errorerd
        public int EndTime => endTime;

        // time since start if stil running
        // or total time taken to download or fail
        public int ElapsedTime => (EndTime == -1) ? DateTime.Now.Millisecond - StartTime : EndTime - StartTime;
        
        // constructor to feed in pending urls
        public GroupDownloader(List<string> urls = null)
        {
            if (urls != null)
            {
                foreach (var str in urls) 
                    PendingURLS.Add(str);
            }
        }

        // starts a group download with PendingURLS
        public bool Download()
        {
            if (PendingURLS.Count == 0 || Downloading) 
                return false;

            this.initialCount = PendingURLS.Count;
            this.downloading = true;
            this.startTime = DateTime.Now.Millisecond;
            DownloadRecursive();
            return true;
        }

        // recursive enumerator for downloading all valid file-containing urls in 'PendingURLS'
        private async void DownloadRecursive()
        {
            if (PendingURLS.Count == 0)
            { // handle no URL case
                HandleCancel();
                return;
            }

            string uri = PendingURLS[0];
            if ((OnURIToFilename == null && !UseURIFilenameMap) || (UseURIFilenameMap && !URIFilenameMap.ContainsKey(uri)))
            {
                HandleCancel();
                return;
            }

            string fileName = UseURIFilenameMap ? URIFilenameMap[uri] : OnURIToFilename(uri);
            if (fileName == null && AbandonOnFailure)
            {
                HandleCancel();
                return;
            }
            var fileResultPath = Path.Combine(DownloadPath, fileName);
            PendingURLS.RemoveAt(0);
            if (!Downloading)
            { // case cancel invoked
                HandleCancel(uri, fileResultPath);
                if (AbandonOnFailure) 
                    return;
            }

            var uwr = new UnityWebRequest(uri, UnityWebRequest.kHttpVerbGET);
            uwr.timeout = Timeout;
            if (!string.IsNullOrEmpty(this.token))
            {
                uwr.SetRequestHeader("X-AUTH-TOKEN", this.token);
            }
            
            var dh = new DownloadHandlerFile(fileResultPath);
            dh.removeFileOnAbort = true;
            uwr.downloadHandler = dh;
            var operation = uwr.SendWebRequest();
            while (!operation.isDone)
                await Task.Delay(100);

            if (uwr.result != UnityWebRequest.Result.Success || !Downloading)
            { // case network error or cancel invoked
                HandleCancel(uri, fileResultPath);
                if (PendingURLS.Count > 0) DownloadRecursive();
            }
            else
            { // case succcesful download
                progress += (float)(1f / (float)initialCount);
                CompletedURLS.Add(uri);
                CompletedURLPaths.Add(fileResultPath);
                if (PendingURLS.Count > 0)
                { // case more files to download
                    OnDownloadSuccess?.Invoke(DidFinish, uri, fileResultPath);
                    DownloadRecursive();
                }
                else
                { // case no more files to download
                    downloading = false;
                    endTime = DateTime.Now.Millisecond;
                    _elapsedTime = EndTime - StartTime;
                    OnDownloadSuccess?.Invoke(DidFinish, uri, fileResultPath);
                }
            }
        }

        // handles a failure of cancel post-download or pre-download
        private void HandleCancel(string uri = null, string fileResultPath = null)
        {
            endTime = DateTime.Now.Millisecond;
            _elapsedTime = EndTime - StartTime;
            downloading = false;

            if (uri != null) UncompletedURLS.Add(uri);
            if (AbandonOnFailure)
            {
                if (fileResultPath != null) File.Delete(fileResultPath);
                for (int i = CompletedURLPaths.Count - 1; i >= 0; i--)
                {
                    File.Delete(CompletedURLPaths[i]);
                }
                for (int i = PendingURLS.Count - 1; i >= 0; i--)
                {
                    UncompletedURLS.Add(PendingURLS[i]);
                }
                PendingURLS.Clear();
                progress = 0f;
                OnDownloadFailure?.Invoke(DidFinish, uri, fileResultPath);
                return;
            }
            progress += (1 / initialCount); // because this value would not be incremented otherwise
            OnDownloadFailure?.Invoke(DidFinish, uri, fileResultPath);
        }

        // param true to stop a current download
        // NOTE: case enum gets called AFTER this func invokates
        public void Cancel()
        {
            if (!Downloading)
                return;

            downloading = false;
        }

        // resets internal values and allows reuse of this group downloader
        public void Reset()
        {
            if (Downloading) 
                return;

            completedUrls = new List<string>();
            uncompletedUrls = new List<string>();
            progress = 0f;
            initialCount = 0;
            OnURIToFilename = null;
            uriFilenameMap = new Dictionary<string, string>();
        }
    }
}
