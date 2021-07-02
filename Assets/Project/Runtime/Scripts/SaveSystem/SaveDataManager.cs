using System;
using System.IO;
using UnityEngine;

namespace SaveSystem.Data
{
    public class SaveDataManager : MonoBehaviour
    {
        public static event Action DataSavedPrepared;
        public static event Action SaveDataLoaded;

        static SaveDataManager _instance;
        static string fileName = "save";
        static int currentSave = 1;
        static SaveFileType fileType = SaveFileType.dat;

        int saveCount = 1;

        public static string SAVE_PATH => $"{Application.persistentDataPath}/saves/";
        public static string FileName => $"{fileName}_{currentSave:00}.{fileType}";
        public static SaveDataManager Instance => _instance;
        public static SaveDataState Save { get; private set; }
        public static bool IsDataLoaded { get; private set; } = false;


        void Awake()
        {
            if (_instance == null)
                _instance = this;
            else if (_instance != this)
            {
                Debug.LogWarning("Multiple instances of SaveDataManager");
                Destroy(this);
            }
            LoadData();
        }

        void LoadData()
        {
            if (File.Exists(SAVE_PATH + FileName))
            {
                SaveDataState _deserializeSave = SaveLoad.Load<SaveDataState>(SAVE_PATH, FileName);
                if (_deserializeSave.Equals(default(SaveDataState)))
                {
                    Debug.LogError($"Failed to load save file {fileName}.");
                    CreateNewSaveFile();
                }
                else
                {
                    Debug.Log($"Save file {fileName} was loaded.");
                    Save = _deserializeSave;
                }
            }
            else
            {
                Debug.LogWarning($"Save file {fileName} not found.");
                CreateNewSaveFile();
            }
            IsDataLoaded = true;

            SaveDataLoaded?.Invoke();
        }

        void SaveData()
        {
            DataSavedPrepared?.Invoke();
            SaveLoad.Save(Save, SAVE_PATH, FileName);

            Debug.Log($"Saved Data to {SAVE_PATH}{FileName}.");
        }

        public void On_Load_Data() => LoadData();
        public void On_Save_Data() => SaveData();

        void BackupSave()
        {
            string backupFileName;
            string backupSavePath;
            int backupIndex = 1;

            do
            {
                backupSavePath = $"{Application.persistentDataPath}/SaveBackups/";
                backupFileName = $"{FileName}_{backupIndex:00}.bak";

                backupIndex++;
            } while (File.Exists(backupFileName));

            SaveLoad.Save(Save, backupSavePath, backupFileName);

            Debug.Log($"Backed up current save to {backupSavePath}{backupFileName}");
        }


        public void SelectSave(int saveNumber, bool load = false)
        {
            currentSave = saveNumber;
            if (load)
                LoadData();
        }

        void CreateNewSaveFile()
        {
            Debug.LogWarning("No valid Save file found; creating a new save.");

            Save = new SaveDataState();
            SaveDataLoaded?.Invoke();
            SaveData();

            saveCount++;
        }

        public void ResetSave()
        {
            Save = new SaveDataState();
        }
        public void ClearSave()
        {
            SaveLoad.DeleteSave($"{SAVE_PATH}{FileName}");
        }
        public void ClearAllSaves()
        {
            SaveLoad.DeleteAllSaveFilesIn(SAVE_PATH);
        }
    }
}





