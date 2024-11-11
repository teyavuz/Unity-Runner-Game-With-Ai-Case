using System.Collections;
using UnityEngine;

public class ObstacleBounce : MonoBehaviour
{
    [Header("Bounce Settings")]
    [SerializeField] private float bounceForce = 15f;
    [SerializeField] private float navMeshDisableDuration = 1f;
    [SerializeField] private float velocityResetDelay = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            ApplyBounceForce(collision, playerRigidbody);
        }
    }

    private void ApplyBounceForce(Collision collision, Rigidbody playerRigidbody)
    {
        Vector3 bounceDirection = -collision.contacts[0].normal;
        playerRigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        StartCoroutine(ResetVelocityAfterDelay(playerRigidbody, velocityResetDelay));
    }

    private IEnumerator ResetVelocityAfterDelay(Rigidbody playerRigidbody, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerRigidbody.velocity = Vector3.zero;
    }
}
