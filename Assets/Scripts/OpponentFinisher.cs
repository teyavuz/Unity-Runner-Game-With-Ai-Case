using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentFinisher : MonoBehaviour
{
    private int opponentPlayerLayer;

    private void Start()
    {
        opponentPlayerLayer = LayerMask.NameToLayer("Opponent");
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.layer == opponentPlayerLayer)
            {
                Debug.Log("Bir Ai bitirdi!");

                Destroy(other.gameObject);
            }
        }
    }
}

