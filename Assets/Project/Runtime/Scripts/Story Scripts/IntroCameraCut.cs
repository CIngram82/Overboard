using UnityEngine;
using Cinemachine;
using SaveSystem.Data;

public class IntroCameraCut : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera introCamera;
    [SerializeField] GameObject camPointer;
    // [SerializeField] GameObject blur;
    public static bool hasPlayed = false;


    void Start()
    {
        if (!hasPlayed)
        {
            PlayerMovement.canMove = false;
            camPointer.SetActive(false);
            playerCam.Priority = 2;
            introCamera.Priority = 3;
        }
        else
        {
            PlayerMovement.canMove = true;
            playerCam.Priority = 3;
            introCamera.Priority = 2;
            gameObject.SetActive(false);
        }
    }

    public void ChangeToPlayerCam()
    {
        playerCam.Priority = 3;
        introCamera.Priority = 2;
        hasPlayed = true;
        camPointer.SetActive(true);
        PlayerMovement.canMove = true;
        gameObject.SetActive(false);
    }

    void PlayCrash()
    {
        AudioScript._instance.PlaySoundEffect("Crash");
    }

    void SaveData()
    {
        TutorialData data = SaveDataManager.Save.TutorialData;
        data.IntroPlayed = hasPlayed;
    }
    void LoadData()
    {
        TutorialData data = SaveDataManager.Save.TutorialData;
        hasPlayed = data.IntroPlayed;
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
}
