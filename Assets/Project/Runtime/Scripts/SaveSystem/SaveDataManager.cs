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
        static string SAVE_PATH;
        const string FILENAME = "save.dat";

        public static SaveDataManager Instance => _instance;
        public static SaveDataState Save { get; private set; }
        public static bool IsDataLoaded { get; private set; } = false;


        private void Awake()
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

        public static void LoadData()
        {
            SaveDataState _deserializeSave = SaveLoad.Load<SaveDataState>(FILENAME);
            if (File.Exists(SaveLoad.SAVE_PATH))
            {
                if (_deserializeSave.Equals(default(SaveDataState)))
                {
                    Debug.LogError("Failed to load save file.");
                    CreateNewSaveFile();
                }
                else
                {
                    Debug.LogWarning("Save file was loaded.");

                    Save = _deserializeSave;
                }
            }
            else
            {
                Debug.LogError("Save file not found.");
                CreateNewSaveFile();
            }

            IsDataLoaded = true;

            SaveDataLoaded?.Invoke();
        }

        public static void SaveData()
        {
            DataSavedPrepared?.Invoke();

            SaveLoad.Save(Save, FILENAME);

            Debug.Log("Saved Data to persistentDataPath.");
        }

        private void BackupSave()
        {
            string backupFilename;
            int backupIndex = 1;

            do
            {
                backupFilename = Application.persistentDataPath + $"/SaveBackup{backupIndex}.dat";
                backupIndex++;
            } while (File.Exists(backupFilename));

            SaveLoad.Save(Save, backupFilename);

            Debug.Log($"Backed up current save to {backupFilename}");
        }

        private static void CreateNewSaveFile()
        {
            Debug.LogWarning("No valid Save file found; creating a new save.");

            Save = new SaveDataState();

            SaveDataLoaded?.Invoke();
            SaveData();
        }

        public void ClearSaves()
        {
            SaveLoad.DeleteAllSaveFilesIn("");
        }
    }
}





