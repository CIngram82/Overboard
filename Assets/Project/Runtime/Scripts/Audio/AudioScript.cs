using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioScript : MonoBehaviour
{
    public AudioSource thisAudio;
    //public static AudioScript Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = GameObject.FindObjectOfType<AudioScript>();
    //        }
            

    //        return _instance;
    //    }
    //}

    public static AudioScript _instance;
    public List<AudioClip> soundEffects = new List<AudioClip>();
    [SerializeField] List<AudioClip> woodFootsteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> carpetFootsteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> metalFootsteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> waterFootsteps = new List<AudioClip>();
    Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    public AudioClip menuButton;
    public AudioClip titleTheme;
    public List<AudioClip> randomSounds = new List<AudioClip>();
    public float minWaitBetweenPlays = 25f;
    public float maxWaitBetweenPlays = 45f;
    public float waitTimeCountdown = -1f;
    public Scene activeScene;

    private void Awake()
    {
       if(_instance == null)
       {
            _instance = this;
            DontDestroyOnLoad(gameObject);
       }
       else
       {
            Destroy(gameObject);
       }
        
    }

    private void Start()
    {
        CreateKeys();
    }


    public void PlayClip(AudioClip clip)
    {
        //thisAudio.pitch = Random.Range(pitchMin, pitchMax); // This always changes pitch AND tempo because it is changing by sample rate. Did you mean Volume?
        thisAudio.PlayOneShot(clip);
    }

    public void PlaySoundEffect(string soundName)
    {
        //PlayClip(soundEffects[clipIndex]);
        if (sounds.ContainsKey(soundName))
        {
            thisAudio.PlayOneShot(sounds[soundName]);
        }
        else
        {
            Debug.Log("Sound " + soundName + " does not exist in sound lookup");
        }
    }

    public void StopAudio()
    {
        thisAudio.Stop();
    }
    public void PlayCarpetFootsteps()
    {
        float randomVol = Random.Range(.2f, .6f);
        PlayClip(carpetFootsteps[Random.Range(0, carpetFootsteps.Count)]);
    }
    public void PlayWoodFootsteps()
    {
        float randomVol = Random.Range(.2f, .6f);
        PlayClip(woodFootsteps[Random.Range(0, woodFootsteps.Count)]);
    }
    public void PlayMetalFootsteps()
    {
        float randomVol = Random.Range(.2f, .6f);
        PlayClip(metalFootsteps[Random.Range(0, metalFootsteps.Count)]);
    }
    public void PlayWaterFootsteps()
    {
        float randomVol = Random.Range(.2f, .6f);
        PlayClip(waterFootsteps[Random.Range(0, waterFootsteps.Count)]);
    }

    public Scene GetScene()
    {
        activeScene = SceneManager.GetActiveScene();
        return activeScene;
    }

    public void PlayBackgroundMusic()
    {
        if (GetScene().name == "Scene_MainMenu" && !thisAudio.isPlaying)
        {
            StopAllCoroutines();
            Debug.Log("Playing again " + GetScene().name + " " + thisAudio.isPlaying);
            PlayClip(titleTheme);
        }
        else if (GetScene().name == "BetaLevel1" && !thisAudio.isPlaying)
        {
            StartCoroutine(PlayRandomSound());
        }
        else
        {
            StopAllCoroutines();
            Debug.Log("Audio else fallthrough");
        }
    }

   void CreateKeys()
   {
        foreach(AudioClip clip in soundEffects)
        {
            sounds.Add(clip.name, clip);
            Debug.Log(clip.name);
        }
   }
  
    public void PlayItemSound(string itemName)
    {
        switch (itemName)
        {
            case "Safekey1": 
                PlaySoundEffect("Lighter Click");
                break;
            case "Safekey2":
                PlaySoundEffect("Drawer");
                break;
            case "Safekey3":
                PlaySoundEffect("Pen Click");
                break;
        }
    }

    IEnumerator PlayRandomSound()
    {
        while (activeScene.name == "BetaLevel1" && !thisAudio.isPlaying)
        {
            PlayClip(randomSounds[Random.Range(0, randomSounds.Count)]);
            yield return new WaitForSeconds(Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays)); 
        }
        
        yield return null;
    }

}

