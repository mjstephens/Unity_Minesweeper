using System.IO;
using UnityEngine;

namespace Unitysweeper.Serialization
{
    public static class Serializer
    {
        #region Variables

        private const string CONST_dataPathHeader = "data";
        private static string _fullDataDirectoryHeader;

        #endregion Variables


        #region Initialization

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void LoadSerializer()
        {
            // Get full data directory
            _fullDataDirectoryHeader = Path.Combine(Application.persistentDataPath, CONST_dataPathHeader);
            
            // Does this user have save data, or is this the first time they're playing?
            DirectoryInfo d = new DirectoryInfo(_fullDataDirectoryHeader);
            if (!d.Exists)
            {
                d.Create();
            }
        }

        #endregion Initialization
        
        
        #region Serialization

        public static string ReadData(string path)
        {
            string contents = "";
            string filePath = GetAppFileDirectoryPath(path);
            FileInfo f = new FileInfo(filePath);
            if (f.Exists)
            {
                contents = File.ReadAllText(filePath);
            }
            return contents;
        }

        public static void WriteData(string data, string path, bool append = false)
        {
            // Create the file if needed
            string filePath = GetAppFileDirectoryPath(path);
            FileInfo f = new FileInfo(filePath);
            StreamWriter writer = new StreamWriter (filePath, append);
            if (!f.Exists)
            {
                writer = f.CreateText();
                writer.Write (data);
                writer.Close ();
                Debug.Log("FILE CREATED: \n" + filePath);
            }
            else
            {
                writer.Write (data);
                writer.Close ();
            }
        }

        #endregion Serialization
        
        
        #region Utility

        /// <summary>
        /// Returns a fully concatenated path for the given data directory
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string GetAppFileDirectoryPath(string filePath)
        {
            return Path.Combine(_fullDataDirectoryHeader, filePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static FileInfo CreateFile(string filePath)
        {
            FileInfo f = new FileInfo(filePath);
            f.Create();
            return f;
        }

        #endregion Utility
    }
}