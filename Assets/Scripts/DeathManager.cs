using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [Header("Canvas Textes")]
    [SerializeField] private TextMeshProUGUI deathText;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake() 
    {
        deathText.text = PlayerPrefs.GetInt("deathCount").ToString();
        coinText.text = PlayerPrefs.GetInt("coinCount").ToString();
    }
}
