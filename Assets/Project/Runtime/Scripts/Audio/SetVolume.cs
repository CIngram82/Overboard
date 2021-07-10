using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] AudioMixer mixer;

    public void SetMasterVol(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSlider(float volume)
    {
        volume = Mathf.Pow(10, volume / 20);
        slider.value = volume;
    }

    void Start()
    {
        mixer.GetFloat("MasterVol", out float value);
        SetSlider(value);
    }
}
