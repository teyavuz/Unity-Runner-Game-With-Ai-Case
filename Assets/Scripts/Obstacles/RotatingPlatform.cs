using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : Obstacles
{
    [SerializeField] private float rotZ;
    public Rigidbody rb;

    void Update()
    {
        RotateObstacle(gameObject.transform, 0.1f, 0, 0, rotZ);
    }

    /*
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);

            other.transform.rotation = Quaternion.identity;
        }
    }
    */
}
