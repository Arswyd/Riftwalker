using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip gameOverClip;
    [SerializeField] AudioClip gameplayClip;
    [SerializeField] float maxVolume;
    [SerializeField] float pitch = 0.8f;
    [SerializeField] float timeToPitchFade;
    [SerializeField] float timeToCrossFade;
    [SerializeField] bool isGameOverScene;
    static AudioManager instance;
    AudioSource menuSource;
    AudioSource gameplaySource;
    bool isPitchDecreased = false;

    void Awake()
    {
        ManageSingleton();
    }

    void Start()
    {
        CreateAudioSources();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void CreateAudioSources()
    {
        menuSource = gameObject.AddComponent<AudioSource>();
        menuSource.clip = gameOverClip;
        menuSource.volume = isGameOverScene ? maxVolume : 0;
        menuSource.loop  = true;
        menuSource.Play();

        gameplaySource = gameObject.AddComponent<AudioSource>();
        gameplaySource.clip = gameplayClip;
        gameplaySource.volume = isGameOverScene ? 0 : maxVolume;
        gameplaySource.loop  = true;
        gameplaySource.Play();
    }

    public void ModifySourcePitch()
    {
        StartCoroutine(ModifyPitch());

        isPitchDecreased = !isPitchDecreased;
    }

    IEnumerator ModifyPitch()
    {
        float timeElapsed = 0;

        if(isPitchDecreased)
        {
            while(timeElapsed < timeToPitchFade)
            {   
                gameplaySource.pitch = Mathf.Lerp(pitch, 1, timeElapsed / timeToPitchFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while(timeElapsed < timeToPitchFade)
            {
                gameplaySource.pitch = Mathf.Lerp(1, pitch, timeElapsed / timeToPitchFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }

    public void SwapAudioSources()
    {
        StartCoroutine(CrossfadeSources());
        isGameOverScene = !isGameOverScene;
    }

    IEnumerator CrossfadeSources()
    {
        float timeElapsed = 0;

        if(isGameOverScene)
        {
            gameplaySource.pitch = 1;
            isPitchDecreased = false;
            gameplaySource.time = 0;
            while(timeElapsed < timeToCrossFade)
            {   
                gameplaySource.volume = Mathf.Lerp(0, maxVolume, timeElapsed / timeToCrossFade);
                menuSource.volume = Mathf.Lerp(maxVolume, 0, timeElapsed / timeToCrossFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            while(timeElapsed < timeToCrossFade)
            {
                menuSource.volume = Mathf.Lerp(0, maxVolume, timeElapsed / timeToCrossFade);
                gameplaySource.volume = Mathf.Lerp(maxVolume, 0, timeElapsed / timeToCrossFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
