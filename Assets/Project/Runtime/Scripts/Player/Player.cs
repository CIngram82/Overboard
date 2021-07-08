using UnityEngine;
using SaveSystem.Data;
using TMPro;
using InventorySystem;

public class Player : MonoBehaviour
{
    public static Player player; 
    public static Inventory inventory; 
    public static InspectObject inspect; 
    public static Camera inspectCam;
    public static TextMeshProUGUI playerPromptText;
    [SerializeField] Camera _inspectCam;
    [SerializeField] TextMeshProUGUI _playerPromptText;


    void SaveData()
    {
        SaveDataManager.Save.PlayerData = new PlayerData(transform);
    }
    void LoadData()
    {
        PlayerData data = SaveDataManager.Save.PlayerData;

        CameraController.SetCameraRotation(data.Rotation.Get());
        if (data.Position != null)
            transform.position = data.Position.Get();
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
        player = this;
        inventory = GetComponent<Inventory>();
        inspect = GetComponent<InspectObject>();
        inspectCam = _inspectCam;
        playerPromptText = _playerPromptText;

        if (SaveDataManager.IsDataLoaded)
            On_SaveData_Loaded();
    }
}





