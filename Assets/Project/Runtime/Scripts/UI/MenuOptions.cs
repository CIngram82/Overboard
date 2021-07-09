using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
    
    {

    public GameObject controlsPanel;

    
    void Start()
    {
        controlsPanel.SetActive(false);
    }
    
    public void OnOptionsButton()
    {
        controlsPanel.SetActive(true);
    }

    public void OnBackButton()
    {
        controlsPanel.SetActive(false);
    }
    
}
