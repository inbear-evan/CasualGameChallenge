using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Renci.SshNet;
using System;
using System.IO;
using System.Text;
using Renci.SshNet.Sftp;

namespace Yellotail
{
    public class SimpleFTPTest : MonoBehaviour
    {
        private string host = "192.168.1.100";
        private string user = "ftpuser";
        private string pass = "Yello3737";
        private int port = 22022;

        private string file = "iu_coin.mp4";
        private string from = @"d:\";
        private string to = @"./ftp/metaverse/";

        public void UploadFile()
        {
            try
            {
                var client = new SftpClient(host, port, user, pass);
                client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(100);
                client.OperationTimeout = TimeSpan.FromSeconds(100);

                client.Connect();
                using (var inFile = File.OpenRead(Path.Combine(from, file)))
                {
                    client.UploadFile(inFile, Path.Combine(to, file));
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
        public void DownloadFile()
        {
            try
            {
                var client = new SftpClient(host, port, user, pass);
                client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(100);
                client.OperationTimeout = TimeSpan.FromSeconds(100);

                client.Connect();
                using (var outFile = File.OpenWrite(Path.Combine(@"d:\ftp", file)))
                {
                    client.DownloadFile(Path.Combine(to, file), outFile);
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public void ListFiles()
        {
            try
            {
                var client = new SftpClient(host, port, user, pass);
                client.KeepAliveInterval = TimeSpan.FromSeconds(60);
                client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(100);
                client.OperationTimeout = TimeSpan.FromSeconds(100);

                client.Connect();

                var filelist = new StringBuilder();
                foreach (var f in client.ListDirectory(to))
                {
                    filelist.AppendLine(f.FullName);
                }
                Debug.Log(filelist.ToString());                        
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}
