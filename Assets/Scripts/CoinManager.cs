using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [Header("Coin Text")]
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Canvas Coin Image Position")]
    [SerializeField] private GameObject canvasCoinImagePos;

    [Header("Coin Prefab")]
    [SerializeField] private GameObject coinPrefab;

    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private int coinCount = 5;

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

        coinText.text = PlayerPrefs.GetInt("coinCount").ToString();
    }

    public void CoinCollected(GameObject attachedCoin, Vector3 worldToCanvasPosition)
    {
        Debug.Log("Collected");

        MeshRenderer attachedMesh = attachedCoin.GetComponent<MeshRenderer>();
        attachedMesh.enabled = false;

        SphereCollider attachedCollider = attachedCoin.GetComponent<SphereCollider>();
        attachedCollider.enabled = false;


        AudioManager.Instance.PlayEffect(0);


        for (int i = 0; i < coinCount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, canvasCoinImagePos.transform.parent);

            // DAHA İDEAL BİR YÖNTEM VAR MI ARAŞTIR!
            Vector3 offset = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0f);
            coin.transform.position = worldToCanvasPosition + offset;

            coin.transform.DOMove(canvasCoinImagePos.transform.position, animationDuration).OnComplete(() => Destroy(coin));
        }

        attachedCoin.transform.DOMove(attachedCoin.transform.position, animationDuration).OnComplete(() => Destroy(attachedCoin));

        int totalCoin = PlayerPrefs.GetInt("coinCount");
        totalCoin += 5;
        PlayerPrefs.SetInt("coinCount", totalCoin);
        coinText.text = PlayerPrefs.GetInt("coinCount").ToString();
    }

}
