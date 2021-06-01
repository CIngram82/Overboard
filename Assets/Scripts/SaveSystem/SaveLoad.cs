using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem
{
    public enum SaveFileType
    {
        sav,
        txt,
    }

    public class SaveLoad : MonoBehaviour
    {
        static string FolderPath = string.Concat(Application.persistentDataPath, "/saves/");
        static SaveFileType fileType = SaveFileType.txt;

        public static void Save<T>(T saveObject, string key, string folder = "")
        {
            Directory.CreateDirectory(FolderPath);
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fileStream = new FileStream(string.Concat(FolderPath, key, fileType), FileMode.Create))
            {
                formatter.Serialize(fileStream, saveObject);
            }
        }

        public static T Load<T>(string key)
        {
            T returnValue = default(T);
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fileStream = new FileStream(string.Concat(FolderPath, key, fileType), FileMode.Open))
            {
                returnValue = (T)formatter.Deserialize(fileStream);
            }

            return returnValue;
        }

        public static bool SaveExists(string key)
        {
            string path = string.Concat(FolderPath, key, fileType);
            return File.Exists(path);
        }

        public static void DeleteAllSaveFilesIn(string location)
        {
            string path = string.Concat(FolderPath, location);
            DirectoryInfo directory = new DirectoryInfo(path);
            directory.Delete(true);
            Directory.CreateDirectory(path);
        }
    }
}
