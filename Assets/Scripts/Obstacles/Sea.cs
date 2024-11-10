using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : Obstacles
{
    private int ourPlayerLayer;

    private void Start() 
    {
        ourPlayerLayer = LayerMask.NameToLayer("OurPlayer");
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterBase playerController = other.gameObject.GetComponent<CharacterBase>();

            if (playerController != null)
            {
                playerController.TeleportStartPosition();
            }

            if (other.gameObject.layer == ourPlayerLayer)
            {
                DeathManager.Instance.DeathCountUpdater();
            }
        }    
    }
}
