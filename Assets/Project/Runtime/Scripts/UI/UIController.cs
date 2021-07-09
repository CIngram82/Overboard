using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveSystem.Data;

namespace Controllers.UI
{
    public class UIController : MonoBehaviour
    {
        protected virtual void Start()
        {
            AudioScript._instance.PlayBackgroundMusic();
        }
        public static void OnLoadScene(int index)
        {
            AudioScript._instance.StopAudio();
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            SceneManager.LoadScene(index);
                       
        }
       public void OnStart(int index)
       {
            SaveDataManager.Instance.ResetSave();
            OnLoadScene(index);
       }
        public static void OnHelp()
        {
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            SceneManager.LoadScene("Scene_Help");
        }
        public static void OnCredits()
        {
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            SceneManager.LoadScene("Scene_Credits");
        }
        public static void OnMenu()
        {
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            SceneManager.LoadScene("Scene_MainMenu");
        }
        public static void OnReset()
        {
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public static void OnQuit()
        {
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            Application.Quit();
        }
        public void OnFullReset()
        {
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            SaveDataManager.Instance.ResetSave();
            Debug.LogWarning($"Resetting save");
            OnReset();
        }
        public void OnSave()
        {
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
            Debug.LogWarning($"{gameObject.name}: Saving");
            SaveDataManager.Instance.On_Save_Data();
        }
        public static void OnLoadSave(int save)
        {
            AudioScript._instance.PlayClip(AudioScript._instance.menuButton);
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

