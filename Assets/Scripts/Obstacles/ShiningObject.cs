using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiningObject : Obstacles
{
    [SerializeField] private float moveLastLocation;
    [SerializeField] private ParticleSystem ps;

    private void Start()
    {
        MoveObstacle(moveLastLocation, gameObject.transform, 5f);
    }
    void Update()
    {
        RotateObstacle(gameObject.transform, 0.1f, 0,1,0);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var mainModule = ps.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(GetRandomColor());

            //Teleport start

            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.TeleportStartPosition();
            }
        }    
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
