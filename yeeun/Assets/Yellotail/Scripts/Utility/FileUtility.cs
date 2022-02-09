using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Yellotail
{
    public static class FileUtility
    {        
        public static void CreateDirectoryIfDoesNotExist(string dir)
        {
            var dirinfo = new DirectoryInfo(dir);
            

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        public static void ClearDirectory(string path)
        {
            var di = new DirectoryInfo(path);
            if (!di.Exists)
                return;

            foreach (var file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (var directory in di.GetDirectories())
            {
                directory.Delete(true);
            }
        }
        public static void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public static void DeleteFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }

        public static string GetFileNameWithoutExtension(this FileInfo fileinfo)
        {
            return Path.GetFileNameWithoutExtension(fileinfo.Name);
        }

        public static void MoveTo(this FileInfo fileinfo, string tofile, bool overwrite)
        {
            if (overwrite)
            {
                DeleteFile(tofile);
            }

            fileinfo.MoveTo(tofile);
        }
        public static void MoveTo(string sourcefile, string destfile, bool overwrite)
        {
            if (overwrite)
            {
                DeleteFile(destfile);
            }

            File.Move(sourcefile, destfile);
        }
    }
}
