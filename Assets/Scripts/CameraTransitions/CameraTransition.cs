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
    int keyClick;
    bool isMain;

    private void Start()
    {
        keyClick = 0;
        isMain = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.E) && keyClick == 0)
        {
            keyClick++;
            SwitchCameras();
            isMain = !isMain;
            Debug.Log("E is pressed");
        }
        else if (Input.GetKey(KeyCode.E) && keyClick >= 1)
        {
            keyClick = 0;
            SwitchCameras();
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
