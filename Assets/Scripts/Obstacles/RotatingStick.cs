    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RotatingStick : Obstacles
    {
        [SerializeField] private float speed;
        [SerializeField] private float turnDirection;

        void Update()
        {
            RotateObstacle(gameObject.transform, speed, 0, turnDirection, 0);
        }

        private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.TeleportStartPosition();
            }
        }
    }
    }
