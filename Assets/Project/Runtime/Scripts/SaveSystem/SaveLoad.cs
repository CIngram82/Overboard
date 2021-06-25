using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem
{
    public enum SaveFileType
    {
        sav,
        txt,
        dat,
    }

    public class SaveLoad
    {
        /// <summary>
        /// Saves serialized data to file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveObject">Serializable object to save.</param>
        /// <param name="path">File path for save file.</param>
        /// <param name="fileName">Name of save file.</param>
        public static void Save<T>(T saveObject, string path, string fileName)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
            if (File.Exists(path + fileName))
                File.Delete(path + fileName);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(string.Concat(path, fileName), FileMode.Create))
            {
                formatter.Serialize(fileStream, saveObject);
            }
        }
        
        /// <summary>
        /// Loads serialized data from file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">File path for save file.</param>
        /// <param name="fileName">Name of save file.</param>
        /// <returns>Saved serialized object or default if file not found.</returns>
        public static T Load<T>(string path, string fileName)
        {
            if (!File.Exists(path + fileName))
                return default(T);
            BinaryFormatter formatter = new BinaryFormatter();
            // Open the file containing the data that you want to deserialize.
            using (FileStream fileStream = new FileStream(string.Concat(path, fileName), FileMode.Open))
            {
                return (T)formatter.Deserialize(fileStream);
            }
        }

        public static bool SaveExists(string path, string fileName)
        {
            string file = path + fileName;
            return File.Exists(file);
        }

        public static void DeleteSave(string filePath)
        {
            File.Delete(filePath);
        }

        public static void DeleteAllSaveFilesIn(string path)
        {
            Debug.LogWarning($"Deleting all saves in {path}");
            // TODO: Add confirmation
            DirectoryInfo directory = new DirectoryInfo(path);
            directory.Delete(true);
            Directory.CreateDirectory(path);
        }
    }
}
