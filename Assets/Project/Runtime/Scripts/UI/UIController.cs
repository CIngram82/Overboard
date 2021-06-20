using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveSystem.Data;

namespace Controllers.UI
{
    public class UIController : MonoBehaviour
    {
        bool isJournalOpen = false;


        public static void OnLoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
        public static void OnHelp()
        {
            Debug.LogWarning("Scene_Help");
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

        public static void SetAllActive(List<GameObject> objectArray, bool state)
        {
            foreach (GameObject objects in objectArray)
            {
                objects.SetActive(state);
            }
        }

#if UNITY_EDITOR
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnReset();
            }
            else
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                isJournalOpen = !isJournalOpen;
                EventsManager.On_Journal_Open(isJournalOpen);
            }
            else
            if (Input.GetKeyDown(KeyCode.L))
            {
                SaveDataManager.Instance.ClearSaves();
                Debug.LogWarning($"Deleting all saves");
            }
        }
#endif
    }
}
