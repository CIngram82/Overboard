using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class IntroCameraCut : MonoBehaviour
{
  [SerializeField]  CinemachineVirtualCamera playerCam;
   [SerializeField] CinemachineVirtualCamera introCamera;
    [SerializeField] GameObject camPointer;
   [SerializeField] GameObject blur;
   bool hasPlayed = false;

    private void Awake()
    {
        
        if (!hasPlayed)
        {
            //PlayRumble();
            PlayerMovement.canMove = false;
            camPointer.SetActive(false);
            playerCam.Priority = 2;
            introCamera.Priority = 3;
        }
        else
        {
            playerCam.Priority = 3;
            introCamera.Priority = 2;
            gameObject.SetActive(false);
        }
    }


   public void ChangeToPlayerCam()
   {
        playerCam.Priority = 3;
        introCamera.Priority = 2;
        hasPlayed = true;
        camPointer.SetActive(true);
        PlayerMovement.canMove = true;
        gameObject.SetActive(false);
   }

    //void PlayRumble()
    //{
    //    AudioScript._instance.PlaySoundEffect("Rumble");
    //}

    void PlayCrash()
    {
        AudioScript._instance.PlaySoundEffect("Crash");
    }



}
