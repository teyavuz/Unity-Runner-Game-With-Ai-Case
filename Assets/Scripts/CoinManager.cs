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
        attachedCollider.enabled=false;

        for (int i = 0; i < coinCount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, canvasCoinImagePos.transform.parent);
            coin.transform.position = worldToCanvasPosition;

            //To-Do coinler hep beraber gidiyor fixle!
            coin.transform.DOMove(canvasCoinImagePos.transform.position, animationDuration).OnComplete(() => Destroy(coin));
            StartCoroutine(waitMe(0.3f));
        }


        attachedCoin.transform.DOMove(attachedCoin.transform.position, animationDuration).OnComplete(() => Destroy(attachedCoin));
    }

    private IEnumerator waitMe(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
