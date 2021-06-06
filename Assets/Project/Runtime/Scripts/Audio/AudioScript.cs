using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    AudioSource thisAudio;
    [SerializeField] List<AudioClip> soundEffects = new List<AudioClip>();
    [SerializeField] List<AudioClip> playerFootsteps = new List<AudioClip>();

    void Start()
    {
        thisAudio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlaySoundEffect(int clipIndex)
    {
        thisAudio.PlayOneShot(soundEffects[clipIndex]);
    }

    public void StopAudio()
    {
        thisAudio.Stop();
    }

    public void PlayCarpetFootsteps()
    {
        thisAudio.PlayOneShot(playerFootsteps[Random.Range(0, 3)]);
    }
    public void PlayWoodFootsteps()
    {
        thisAudio.PlayOneShot(playerFootsteps[Random.Range(3, 6)]);
    }
    public void PlayMetalFootsteps()
    {
        thisAudio.PlayOneShot(playerFootsteps[Random.Range(6, 9)]);
    }

   

    
}
