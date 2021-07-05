using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    
    public void SetMasterVol(float sliderValue)
    {
     mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }
    
}
