using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : CharacterBase
{
    [Header("Own NavmeshAgent")]
    [SerializeField] private NavMeshAgent navMeshAgent;

    [Header("Destination")]
    [SerializeField] private Transform destination;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentState = CharacterState.Idle;

        navMeshAgent.speed = 0f;

        navMeshAgent.destination = destination.position;
    }

    private void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameStates.race)
            navMeshAgent.speed = 8.5f;

        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speedX = localVelocity.x;
        float speedZ = localVelocity.z;

        animator.SetFloat("Horizontal", speedX);
        animator.SetFloat("Vertical", speedZ);
    }
}
