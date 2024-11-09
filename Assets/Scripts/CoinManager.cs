using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

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

    public void CoinCollected(GameObject attachedCoin)
    {
        Debug.Log("Collected");
        Destroy(attachedCoin);
    }
}
