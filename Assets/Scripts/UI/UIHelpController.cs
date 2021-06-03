using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class UIHelpController : UIController
    {
        [SerializeField] GameObject panelsRoot;
        [SerializeField] GameObject buttonsRoot;
        //[SerializeField] Button nextButton;
        //[SerializeField] Button backButton;

        List<Button> helpButtons;
        List<GameObject> helpPanels;
        int panelIndex = 0;


        /// <summary>
        /// Switches panel to next panel in helpPanels.
        /// </summary>
        public void OnNext()
        {
            panelIndex++;
            SwitchPanel();

            if (panelIndex == helpPanels.Count - 1)
            {
                helpButtons[1].interactable = false;
            }
            helpButtons[0].interactable = true;
        }
        /// <summary>
        /// Switches panel to previous panel in helpPanels.
        /// </summary>
        public void OnBack()
        {
            panelIndex--;
            SwitchPanel();

            if (panelIndex == 0)
            {
                helpButtons[0].interactable = false;
            }
            helpButtons[1].interactable = true;
        }
        /// <summary>
        /// Switches panel to the panel at index in helpPanels.
        /// </summary>
        /// <param name="index"></param>
        public void OnButtonSelect(int index)
        {
            if (helpPanels.Count != helpButtons.Count) throw new System.MissingMemberException("Number of buttons and panels must be equal."); 

            SwitchPanel();

            helpButtons[panelIndex].interactable = true;
            helpButtons[index].interactable = false;
            panelIndex = index;
        }
        /// <summary>
        /// Sets panel at index as the only active panel.
        /// </summary>
        void SwitchPanel()
        {
            SetAllActive(helpPanels, false);
            helpPanels[panelIndex].SetActive(true);
        }

        List<T> LoadObjList<T>(GameObject root)
        {
            List<T> objects = new List<T>();
            foreach (Transform child in root.transform)
            {
                objects.Add(child.GetComponent<T>());
            }
            return objects;
        }

        void Start()
        {
            helpPanels = LoadObjList<GameObject>(panelsRoot);
            helpButtons = LoadObjList<Button>(panelsRoot);
            SwitchPanel();
            helpButtons[0].interactable = false;
        }
    }
}
