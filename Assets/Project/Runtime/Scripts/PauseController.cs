using UnityEngine;

public class PauseController : MonoBehaviour
{
    static bool isPaused = false;
    public static bool IsPaused => isPaused;

    [SerializeField] GameObject _pausePanel;


    public void On_Game_Paused(bool paused)
    {
        EventsManager.CameraSwitched(paused);
        _pausePanel.SetActive(paused);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = !isPaused;
            On_Game_Paused(isPaused);
        }
#else
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            On_Game_Paused(isPaused);
        }
#endif
    }
    void Start()
    {
        _pausePanel.SetActive(isPaused);
    }
}





