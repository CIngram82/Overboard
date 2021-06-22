using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;


    public void On_Game_Paused(bool paused)
    {
        //Time.timeScale = paused ? 0 : 1;

        _pausePanel.SetActive(!_pausePanel.activeSelf);
        CameraController.SetCursorLockMode(paused);
    }

    void SubToEvents(bool subscribe)
    {
        EventsManager.GamePaused -= On_Game_Paused;

        if (subscribe)
        {
            EventsManager.GamePaused += On_Game_Paused;
        }
    }

    #region Mono
    void OnEnable()
    {
        SubToEvents(true);
    }
    void OnDisable()
    {
        SubToEvents(false);
    }

    void Start()
    {
        _pausePanel.SetActive(false);
    }
    #endregion
}





