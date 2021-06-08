using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
public class CameraTransition : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera gearCam;
    bool isMain;
    bool atGears;

    private void Start()
    {
        isMain = true;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E))
        {
            SwitchCameras();
            Debug.Log("E is pressed");
        }

    }


    void SwitchCameras()
    {
        Debug.Log("Switch");
        if (isMain)
        {
            gearCam.Priority = 2;
            Debug.Log(gearCam.Priority);
            mainCam.Priority = 1;
            isMain = false;
        }
        else
        {
            gearCam.Priority = 1;
            mainCam.Priority = 2;
            isMain = true;
        }
    }

}
