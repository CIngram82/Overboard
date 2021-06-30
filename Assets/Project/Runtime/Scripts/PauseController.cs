using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;

    bool isPaused;


    public void On_Game_Paused(bool paused)
    {
        _pausePanel.SetActive(paused);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            On_Game_Paused(isPaused);
        }
    }
    void Start()
    {
        _pausePanel.SetActive(false);
    }
}





