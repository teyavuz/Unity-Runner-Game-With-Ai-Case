using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : Obstacles
{
    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.TeleportStartPosition();
            }
        }    
    }
}
