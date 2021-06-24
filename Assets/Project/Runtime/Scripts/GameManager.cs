using UnityEngine;
using SaveSystem.Data;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool GearPuzzleCompleted;
    public bool PipePuzzleCompleted;
    public float WaterLevel;


    void SaveData()
    {
        SaveDataManager.Save.GameData = new GameData()
        {
            IsGearsCompleted = false,
            IsPipesCompleted = false,
            WaterLevel = 0.0f,
        };
    }
    void LoadData()
    {
        GameData data = SaveDataManager.Save.GameData;
        GearPuzzleCompleted = data.IsGearsCompleted;
        PipePuzzleCompleted = data.IsPipesCompleted;
        WaterLevel = data.WaterLevel;
    }

    void On_Gear_Puzzle_Completed(bool isCompleted)
    {
        GearPuzzleCompleted = isCompleted;
        if (isCompleted)
        {
            SaveDataManager.Instance.On_Save_Data();
        }
    }
    void On_Pipe_Puzzle_Completed(bool isCompleted)
    {
        PipePuzzleCompleted = isCompleted;
        if (isCompleted)
        {
            SaveDataManager.Instance.On_Save_Data();
        }
    }
    void On_SaveData_Loaded() => LoadData();
    void On_SaveData_PreSave() => SaveData();

    void SubToEvents(bool subscribe)
    {
        GearPuzzle.GearPuzzleManager.PuzzleCompleted -= On_Gear_Puzzle_Completed;
        PipePuzzle.PipeGameManager.PuzzleCompleted -= On_Pipe_Puzzle_Completed;
        SaveDataManager.SaveDataLoaded -= On_SaveData_Loaded;
        SaveDataManager.DataSavedPrepared -= On_SaveData_PreSave;

        if (subscribe)
        {
            GearPuzzle.GearPuzzleManager.PuzzleCompleted += On_Gear_Puzzle_Completed;
            PipePuzzle.PipeGameManager.PuzzleCompleted += On_Pipe_Puzzle_Completed;
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
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Debug.LogWarning("Multiple instances of GameManager");
            Destroy(this);
        }
    }
}
