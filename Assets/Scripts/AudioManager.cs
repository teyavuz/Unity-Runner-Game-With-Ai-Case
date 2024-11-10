using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    public AudioClip[] musics;
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

    public void PlayMusic(int audioCount)
    {
        audioSource.clip = musics[audioCount];
        audioSource.Play();
    }

    
    public void CoinPickup()
    {
        audioEffect.Play();
    }
}

