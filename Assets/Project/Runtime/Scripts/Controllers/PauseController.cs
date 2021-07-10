using UnityEngine;

public class PauseController : MonoBehaviour
{
    static bool isPaused;
    public static bool IsPaused => isPaused;

    [SerializeField] GameObject _pausePanel;


    public void PauseGame()
    {
        isPaused = !isPaused;
        On_Game_Paused(isPaused);
    }

    public void On_Game_Paused(bool paused)
    {
        EventsManager.CameraSwitched(paused);
        _pausePanel.SetActive(paused);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space) && !FindObjectOfType<ClueInventoryUI>().isJournalOpen)
        {
            PauseGame();
        }
#else
        if (Input.GetKeyDown(KeyCode.Escape) && !FindObjectOfType<ClueInventoryUI>().isJournalOpen)
        {
            isPaused = !isPaused;
            On_Game_Paused(isPaused);
        }
#endif
    }

    void Start()
    {
        isPaused = false;
        _pausePanel.SetActive(isPaused);
    }
}





