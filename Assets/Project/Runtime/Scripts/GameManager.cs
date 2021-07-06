using UnityEngine;
using SaveSystem.Data;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] bool pipeFirstCompletion = false;
    [SerializeField] bool gearFirstCompletion = false;
    
    public bool GearPuzzleCompleted { get; private set; }
    public bool PipePuzzleCompleted { get; private set; }
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
        gearFirstCompletion = GearPuzzleCompleted = data.IsGearsCompleted;
        pipeFirstCompletion = PipePuzzleCompleted = data.IsPipesCompleted;
        FindObjectOfType<WaterLevel>().SetWaterLevel(data.WaterLevel);
    }

    void On_Gear_Puzzle_Completed(bool isCompleted)
    {
        if (isCompleted && !gearFirstCompletion)
        {
            gearFirstCompletion = true;
            FindObjectOfType<WaterLevel>().TriggerWaterRise();
            FindObjectOfType<CameraTransition>().SwitchCameras();
            SaveDataManager.Instance.On_Save_Data();
            GearPuzzleCompleted = isCompleted;
        }
    }
    void On_Pipe_Puzzle_Completed(bool isCompleted)
    {
        if (isCompleted && !pipeFirstCompletion)
        {
            pipeFirstCompletion = true;
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
        InitSingleton(this);
    }
}
