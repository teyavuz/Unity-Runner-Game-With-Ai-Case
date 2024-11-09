using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinManager : MonoBehaviour
{
    [Min(5)]
    [SerializeField] private int MaxCoinCounts = 10;
    [SerializeField] private Transform targetLocation;

    [Range(0, 50)]
    [SerializeField] private float SpawnCircleRadius = 10;

    [SerializeField] private GameObject coinPrefab;


    private Camera m_Camera;
    
    [Range(0,2)]
    [SerializeField] private float coinFlyDuration = 0.5f;
    [Range(0,1)]
    [SerializeField] private float coinFlyDelay =0.03f;
    [SerializeField] private Ease CoinFlyEase = Ease.OutCubic;

    

    private void Awake() 
    {
        m_Camera = Camera.main;
    }

    public void PlayAnim(int count, Vector3 worldPosition)
    {
        count = Mathf.Min(count, MaxCoinCounts);

        Vector3 startPosition = m_Camera.WorldToScreenPoint(worldPosition);

        Vector3 endPosition = targetLocation.position;


        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = Random.insideUnitCircle * SpawnCircleRadius;

            GameObject coin = Instantiate(coinPrefab, startPosition + randomOffset, Quaternion.identity, transform);
            coin.transform.SetAsFirstSibling();

            coin.transform.DOMove(endPosition, coinFlyDuration).SetEase(CoinFlyEase).SetDelay(coinFlyDelay * i).OnComplete(() => Destroy(coin)).Play();
        }
    }
}
