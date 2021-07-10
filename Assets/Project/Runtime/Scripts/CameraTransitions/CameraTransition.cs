using System;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraTransition : MonoBehaviour
{
    public Action CameraEntered;

    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera transitionCamera;
    [SerializeField] int startingPriority;
    [SerializeField] TextMeshProUGUI playerPrompt;
    [SerializeField] bool locked;
    bool hasBeenPrompted;
    bool canTransition;
    bool isMain;



    void Start()
    {
        canTransition = false;
        hasBeenPrompted = false;
        startingPriority = transitionCamera.Priority;
        isMain = true;
    }
    private void Update()
    {
        if (canTransition && Input.GetKeyUp(KeyCode.E) && !locked)
        {
            canTransition = false;
            SwitchCameras();
            CameraEntered?.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        canTransition = true;
    }

    void OnTriggerStay(Collider other)
    {
        if (!hasBeenPrompted)
        {
            playerPrompt.text = "Press E to toggle camera";
        }
        if (!locked && Input.GetKeyDown(KeyCode.E))
        {
            // SwitchCameras();
            // CameraEntered?.Invoke();
            hasBeenPrompted = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        canTransition = false;
        playerPrompt.text = string.Empty;
        hasBeenPrompted = true;
    }

    public void LockPuzzle(bool locked)
    {
        this.locked = locked;
    }
    public void LockCamera(bool locked)
    {
        this.locked = locked;
        SwitchCameras();
    }

    public void SwitchCameras()
    {

        hasBeenPrompted = true;
        playerPrompt.text = string.Empty;

        if (isMain)
        {

            transitionCamera.Priority = 3;
            mainCam.Priority = startingPriority;
            isMain = false;

            GameManager.Instance.inPuzzleView = true;
            EventsManager.On_Camera_Switched(true);
            canTransition = true;
        }
        else
        {

            transitionCamera.Priority = startingPriority;
            mainCam.Priority = 3;
            isMain = true;

            GameManager.Instance.inPuzzleView = false;
            EventsManager.On_Camera_Switched(false);
            canTransition = true;
        }
    }

    void waitForTransition()
    {
        canTransition = true;
    }

    void SubToEvents(bool subscribe)
    {
        EventsManager.PuzzleUnlocked -= LockPuzzle;

        if (subscribe)
        {
            EventsManager.PuzzleUnlocked += LockPuzzle;
        }
    }

    void OnEnable()
    {
        SubToEvents(true);
    }
    void OnDisable()
    {
        SubToEvents(false);
    }
}
