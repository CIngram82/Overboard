using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    AudioSource thisAudio;
    [SerializeField] List<AudioClip> soundEffects = new List<AudioClip>();
    [SerializeField] List<AudioClip> woodFootsteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> carpetFootsteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> metalFootsteps = new List<AudioClip>();
    [SerializeField] float pitchMax = 1.2f;
    [SerializeField] float pitchMin = 0.8f;
    void Start()
    {
        thisAudio = gameObject.GetComponent<AudioSource>();
    }

    private void PlayClip(AudioClip clip)
    {
        thisAudio.pitch = Random.Range(pitchMin, pitchMax);
        thisAudio.PlayOneShot(clip);
    }

    public void PlaySoundEffect(int clipIndex)
    {
        PlayClip(soundEffects[clipIndex]);
    }

    public void StopAudio()
    {
        thisAudio.Stop();
    }
    public void PlayCarpetFootsteps()
    {
        PlayClip(carpetFootsteps[Random.Range(0, carpetFootsteps.Count)]);
    }
    public void PlayWoodFootsteps()
    {
        PlayClip(woodFootsteps[Random.Range(0, woodFootsteps.Count)]);
    }
    public void PlayMetalFootsteps()
    {
        PlayClip(metalFootsteps[Random.Range(0, metalFootsteps.Count)]);
    }
}
