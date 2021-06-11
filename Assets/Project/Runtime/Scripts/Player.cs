using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem.Data;

public class Player : MonoBehaviour
{


    void SaveData()
    {
        SaveDataManager.Save.PlayerData = new PlayerData()
        {

        };
    }
    void LoadData()
    {
        PlayerData data = SaveDataManager.Save.PlayerData;

    }

    void On_SaveData_Loaded() => LoadData();
    void On_SaveData_PreSave() => SaveData();

    void SubToEvents(bool subscribe)
    {
        SaveDataManager.SaveDataLoaded -= On_SaveData_Loaded;
        SaveDataManager.DataSavedPrepared -= On_SaveData_PreSave;

        if (subscribe)
        {
            SaveDataManager.SaveDataLoaded += On_SaveData_Loaded;
            SaveDataManager.DataSavedPrepared += On_SaveData_PreSave;
        }
    }

    void OnEnable()
    {
        SubToEvents(true);
    }
    void OnDisable()
    {
        SubToEvents(false);
    }
    void Awake()
    {
        if (SaveDataManager.IsDataLoaded)
            On_SaveData_Loaded();
    }
}





