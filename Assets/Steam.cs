using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam : MonoBehaviour
{
    ParticleSystem steam;

    private void Start()
    {
        steam = GetComponentInChildren<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {   

        if(GameManager.Instance.PipePuzzleCompleted && !steam.isPlaying)
        {
            steam.Play();
        }
    }
}
