using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveSystem.Data;

namespace Controllers.UI
{
    public class UIController : MonoBehaviour
    {
        bool _isJournalOpen = false;
        bool _isPaused = false;

        public void Start()
        {
            AudioScript._instance.PlayBackgroundMusic();
            Debug.Log("Start");
        }
        public static void OnLoadScene(int index)
        {
            SceneManager.LoadScene(index);
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
        }
        public static void OnHelp()
        {
            Debug.LogWarning("Scene_Help");
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            SceneManager.LoadScene("Scene_Help");
        }
        public static void OnCredits()
        {
            Debug.LogWarning("Loading Scene_Credits");
            SceneManager.LoadScene("Scene_Credits");
        }
        public static void OnMenu()
        {
            Debug.LogWarning("Loading Scene_MainMenu");
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            SceneManager.LoadScene("Scene_MainMenu");
        }
        public static void OnReset()
        {
            Debug.LogWarning("Reset");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public static void OnQuit()
        {
            Debug.LogWarning("Quit");
            Application.Quit();
        }
        public static void OnFullReset()
        {
            SaveDataManager.Instance.ClearAllSaves();
            Debug.LogWarning($"Deleting all saves");
            OnReset();
        }
        public static void OnSave()
        {
            SaveDataManager.Instance.On_Save_Data();
        }
        public static void OnLoadSave(int save)
        {
            SaveDataManager.Instance.SelectSave(save);
        }

        public void OnJournalOpen(bool isJournalOpen)
        {
            CameraController.SetCursorLockMode(isJournalOpen);
            EventsManager.On_Journal_Opened(isJournalOpen);
        }
        void OnPause(bool isPaused)
        {
            Debug.Log("Paused");
            EventsManager.On_Game_Paused(isPaused);
        }

        public static void SetAllActive(List<GameObject> objectArray, bool state)
        {
            foreach (GameObject objects in objectArray)
            {
                objects.SetActive(state);
            }
        }

//#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnReset();
            }
            else
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                _isJournalOpen = !_isJournalOpen;
                OnJournalOpen(_isJournalOpen);
            }
            else
            if (Input.GetKeyDown(KeyCode.Period))
            {
                OnFullReset();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _isPaused = !_isPaused;
                OnPause(_isPaused);
            }
        }
//#endif
    }
}

