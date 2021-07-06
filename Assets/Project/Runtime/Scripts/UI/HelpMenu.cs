using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{

    public GameObject controlsPanel;
    public GameObject gameplayPanel;
    public GameObject optionsPanel;
    public Button controlsButton;
    public Button gameplayButton;
    public Button optionsButton;

    // Start is called before the first frame update
    void Start()
    {
        controlsPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void OnOptionsButton()
    {
        controlsPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OnControlsButton()
    {
        controlsPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    public void OnGameplayButton()
    {
        controlsPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }


}
