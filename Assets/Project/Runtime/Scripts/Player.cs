using UnityEngine;
using SaveSystem.Data;

public class Player : MonoBehaviour
{
    Camera _playerCamera;

    public static Camera Camera;


    void SaveData()
    {
        SaveDataManager.Save.PlayerData = new PlayerData(transform);
    }
    void LoadData()
    {
        PlayerData data = SaveDataManager.Save.PlayerData;
        if (data == null)
            return;
        transform.position = data.Position.Get();
        transform.eulerAngles = data.Rotation.Get();
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
        Camera = _playerCamera ? _playerCamera : Camera.main;
    }
}





