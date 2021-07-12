using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveSystem.Data;

namespace Controllers.UI
{
    public class UIController : MonoBehaviour
    {
        private void Start()
        {
            if (AudioScript._instance)
            {
                AudioScript._instance.PlayBackgroundMusic();
            }
        }
        public static void OnLoadScene(int index)
        {
            if (AudioScript._instance)
            {
                AudioScript._instance.StopAudio();
                AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            }
            SceneManager.LoadScene(index);

        }
        public void OnStart(int index)
        {
            SaveDataManager.Instance.ResetSave();
            OnLoadScene(index);
        }
        public static void OnHelp()
        {
            if (AudioScript._instance)
            {
                AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            }
            SceneManager.LoadScene("Scene_Help");
        }
        public static void OnCredits()
        {
            if (AudioScript._instance)
            {
                AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            }
            SceneManager.LoadScene("Scene_Credits");
        }
        public static void OnMenu()
        {
            if (AudioScript._instance)
            {
                AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            }
            SceneManager.LoadScene("Scene_MainMenu");
        }
        public void OnReset()
        {
            if (AudioScript._instance)
            {
                AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public static void OnQuit()
        {
            if (AudioScript._instance)
            {
                AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            }
            Application.Quit();
        }
        public void OnFullReset()
        {
            if (AudioScript._instance)
            {
                AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            }
            SaveDataManager.Instance.ResetSave();
            Debug.LogWarning($"Resetting save");
            OnReset();
        }
        public void OnSave()
        {
            if (AudioScript._instance)
            {
                AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            }
            Debug.LogWarning($"{gameObject.name}: Saving");
            SaveDataManager.Instance.On_Save_Data();
        }
        public static void OnLoadSave(int save)
        {
            if (AudioScript._instance)
            {
                AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            }
            SaveDataManager.Instance.SelectSave(save);
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
            if (Input.GetKeyDown(KeyCode.Period))
            {
                OnFullReset();
            }
        }
#endif
    }
}

