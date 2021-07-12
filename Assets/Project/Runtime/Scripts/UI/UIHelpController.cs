using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class UIHelpController : UIController
    {
        [SerializeField] Transform panelsRoot;
        [SerializeField] Transform buttonsRoot;
        //[SerializeField] Button nextButton;
        //[SerializeField] Button backButton;

        List<GameObject> helpPanels;
        List<Button> helpButtons;
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

            helpButtons[panelIndex].interactable = true;
            helpButtons[index].interactable = false;
            panelIndex = index;

            SwitchPanel();
        }
        /// <summary>
        /// Sets panel at index as the only active panel.
        /// </summary>
        void SwitchPanel()
        {
            SetAllActive(helpPanels, false);
            helpPanels[panelIndex].SetActive(true);
        }

        List<T> LoadObjList<T>(Transform root) where T : Component
        {
            List<T> components = new List<T>();
            foreach (Transform child in root)
            {
                if (child.TryGetComponent(out T component))
                {
                    components.Add(component);
                }
            }
            return components;
        }

        void Start()
        {
            helpPanels = LoadObjList<CanvasRenderer>(panelsRoot).ConvertAll(t => t.gameObject);
            helpButtons = LoadObjList<Button>(buttonsRoot);
            SwitchPanel();
            helpButtons[0].interactable = false;
        }
    }
}
