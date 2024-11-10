using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStick : Obstacles
{
    [SerializeField] private float speed;
    [SerializeField] private float turnDirection;

    private int ourPlayerLayer;

    private void Start() 
    {
        ourPlayerLayer = LayerMask.NameToLayer("OurPlayer");
    }

    void Update()
    {
        RotateObstacle(gameObject.transform, speed, 0, turnDirection, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterBase playerController = collision.gameObject.GetComponent<CharacterBase>();

            if (playerController != null)
            {
                playerController.TeleportStartPosition();
            }

            if (collision.gameObject.layer == ourPlayerLayer)
            {
                DeathManager.Instance.DeathCountUpdater();
            }
        }
    }
}
