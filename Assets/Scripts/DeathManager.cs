using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [Header("Canvas Textes")]
    [SerializeField] private TextMeshProUGUI deathText;

    public static DeathManager Instance;
    
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

        deathText.text = PlayerPrefs.GetInt("deathCount").ToString();
    }

    public void DeathCountUpdater()
    {
        int deathCount = PlayerPrefs.GetInt("deathCount");
        deathCount++;
        PlayerPrefs.SetInt("deathCount", deathCount);
        deathText.text = deathCount.ToString();
    }

}
