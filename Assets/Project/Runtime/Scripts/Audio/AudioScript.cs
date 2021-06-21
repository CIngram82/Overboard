using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{
    public static AudioSource thisAudio;
    public static AudioScript audioscript;
    public List<AudioClip> soundEffects = new List<AudioClip>();
    [SerializeField] List<AudioClip> woodFootsteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> carpetFootsteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> metalFootsteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> waterFootsteps = new List<AudioClip>();
    [SerializeField] float pitchMax = 1.2f;
    [SerializeField] float pitchMin = 0.8f;
    public List<AudioClip> randomSounds = new List<AudioClip>();
    public float minWaitBetweenPlays = 25f;
    public float maxWaitBetweenPlays = 45f;
    public float waitTimeCountdown = -1f;

    void Start()
    {
        thisAudio = gameObject.GetComponent<AudioSource>();
        audioscript = this;
    }

    private void Update()
    {
        if (waitTimeCountdown < 0f)
        {
            PlayClip(randomSounds[Random.Range(0, randomSounds.Count)]);
            waitTimeCountdown = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
        }
        else
        {
            waitTimeCountdown -= Time.deltaTime;
        }

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
    public void PlayWaterFootsteps()
    {
        PlayClip(waterFootsteps[Random.Range(0, waterFootsteps.Count)]);
    }

    IEnumerator PlayandLoad(int index)
    {
        PlayClip(soundEffects[13]);
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(index);
    }

    public void PlaySoundButton(int index)
    {
        StartCoroutine(PlayandLoad(index));
    }


}

