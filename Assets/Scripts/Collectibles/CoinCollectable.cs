using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinCollectable : MonoBehaviour
{
    [Header("Coin Visual")]
    [SerializeField] private float turnSpeed;

    private Vector3 coinsCanvasPos;
    private Camera mainCam;

    private void Start() 
    {
        coinsCanvasPos = gameObject.transform.position;
        mainCam = Camera.main;
    }

    private void Update() 
    {
        RotateObject();
    }

    private void RotateObject()
    {
        gameObject.transform.DORotate(gameObject.transform.localRotation.eulerAngles + new Vector3(0, 1, 0), turnSpeed);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CoinManager.Instance.CoinCollected(gameObject, mainCam.WorldToScreenPoint(coinsCanvasPos));
        }
    }
}
