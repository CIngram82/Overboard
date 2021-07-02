using UnityEngine;
using SaveSystem.Data;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool GearPuzzleCompleted { get; private set; }
    private bool gearFirstCompleation = false;
    public bool PipePuzzleCompleted { get; private set; }
    private bool pipeFirstCompleation = false;

    public bool inPuzzleView = false;

    void SaveData()
    {
        SaveDataManager.Save.GameData = new GameData()
        {
            IsGearsCompleted = false,
            IsPipesCompleted = false,
        };
    }
    void LoadData()
    {
        GameData data = SaveDataManager.Save.GameData;
        gearFirstCompleation =  GearPuzzleCompleted = data.IsGearsCompleted;
        pipeFirstCompleation = PipePuzzleCompleted = data.IsPipesCompleted;
        FindObjectOfType<WaterLevel>().SetWaterLevel(data.WaterLevel);
    }

    void On_Gear_Puzzle_Completed(bool isCompleted)
    {
        if(isCompleted && !gearFirstCompleation)
        {
            gearFirstCompleation = true;
            FindObjectOfType<WaterLevel>().TriggerWaterRise();
            FindObjectOfType<CameraTransition>().SwitchCameras();
        }
        GearPuzzleCompleted = isCompleted;
        if (isCompleted)
        {
            SaveDataManager.Instance.On_Save_Data();
        }
    }
    void On_Pipe_Puzzle_Completed(bool isCompleted)
    {
        if (isCompleted && !pipeFirstCompleation)
        {
            pipeFirstCompleation = true;
            FindObjectOfType<WaterLevel>().TriggerWaterRise();
            FindObjectOfType<CameraTransition>().SwitchCameras();
        }
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
