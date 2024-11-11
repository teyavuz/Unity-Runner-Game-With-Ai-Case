using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    [SerializeField] private AudioClip[] musics;
    [SerializeField] private AudioClip[] effects;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioEffect;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update() 
    {
        if (GameManager.Instance.gameState == GameManager.GameStates.startCountdown)
        {
            audioSource.volume = 1f;
        }
        else if(GameManager.Instance.gameState == GameManager.GameStates.race)
        {
            audioSource.volume = 0.5f;
        }
        else if(GameManager.Instance.gameState == GameManager.GameStates.painting)
        {
            audioSource.volume = 0.35f;
        }
    }

    public void PlayMusic(int audioCount)
    {
        audioSource.clip = musics[audioCount];
        audioSource.Play();
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void UnpauseMusic()
    {
        audioSource.UnPause();
    }

    
    public void PlayEffect(int audioCount)
    {
        audioEffect.clip = effects[audioCount];
        audioEffect.Play();
    }
}

