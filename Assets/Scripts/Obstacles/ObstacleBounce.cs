using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBounce : MonoBehaviour
{
    public float bounceForce = 15f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                Vector3 bounceDirection = -collision.contacts[0].normal;
                playerRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
            }
        }
    }
}
