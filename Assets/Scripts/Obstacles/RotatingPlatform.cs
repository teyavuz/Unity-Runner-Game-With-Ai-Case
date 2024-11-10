using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : Obstacles
{
    [Header("Rotate Speed")]
    [SerializeField] private float speed = 5.8f;
    [Header("Rotation Side 0=Left - 1=Right")] 
    [Range(0,1)]
    [SerializeField] private int rotation;

    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody ? _rigidbody : (_rigidbody = GetComponent<Rigidbody>());

    private void FixedUpdate()
    {
        Vector3 pos = Rigidbody.position;

        if(rotation == 0)
        Rigidbody.position -= Vector3.left * Time.fixedDeltaTime * speed;
        else if(rotation == 1)
        Rigidbody.position -= Vector3.right * Time.fixedDeltaTime * speed;

        _rigidbody.MovePosition(pos);

        transform.Rotate(Vector3.forward, speed * Time.deltaTime * 8);
    }


    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        if (!TryGetComponent(out CharacterBase player))
            return;

        player.rigidBody.velocity = Vector3.zero;
        player.isGrounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        if (!TryGetComponent(out CharacterBase player))
            return;

        player.isGrounded = false;
    }
}
