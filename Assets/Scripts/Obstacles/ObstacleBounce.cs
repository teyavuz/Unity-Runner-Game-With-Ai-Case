using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

                if (collision.gameObject.layer == LayerMask.NameToLayer("Opponent"))
                {
                    NavMeshAgent navMeshAgent = collision.gameObject.GetComponent<NavMeshAgent>();

                    if (navMeshAgent != null)
                    {
                        StartCoroutine(DisableNavMeshAgentTemporarily(navMeshAgent, playerRigidbody));
                    }
                }
            }
        }
    }

    private IEnumerator DisableNavMeshAgentTemporarily(NavMeshAgent navMeshAgent, Rigidbody playerRigidbody)
    {
        navMeshAgent.enabled = false;

        yield return new WaitForSeconds(1f);

        navMeshAgent.enabled = true;
        playerRigidbody.velocity = Vector3.zero;
    }
}
