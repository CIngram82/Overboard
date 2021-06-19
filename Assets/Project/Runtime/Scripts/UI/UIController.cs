using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.UI
{
    public class UIController : MonoBehaviour
    {

        public static void OnLoadScene(int index)
        {
            // SceneManager.LoadScene(index);
            AudioScript.audioscript.PlaySoundButton(index);
        }
        public static void OnHelp()
        {
            Debug.LogWarning("Scene_Help");
            // SceneManager.LoadScene("Scene_Help");
            OnLoadScene(1);
        }
        public static void OnCredits()
        {
            Debug.LogWarning("Loading Scene_Credits");
            //SceneManager.LoadScene("Scene_Credits");
            OnLoadScene(2);
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
        }
#endif
    }
}

