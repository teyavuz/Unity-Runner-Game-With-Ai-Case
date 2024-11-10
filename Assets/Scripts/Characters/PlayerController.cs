using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : CharacterBase
{
    [Header("Player Special Settings")]
    private float x, z;
    private Vector3 move = Vector3.zero;

    [Header("Joystick")]
    [SerializeField] private FixedJoystick joystick;

    [Header("Ground Layer")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Camera Connections")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private float currentPov;

    private void Awake() 
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        HandleStateChanges();
        UpdateAnimatorParameters();
        UpdateCameraFOV();
    }

    private void FixedUpdate() 
    {
        if(GameManager.Instance.gameState == GameManager.GameStates.race)
            Movement();    
    }

    private void Movement()
    {
        x = joystick.Horizontal;
        z = joystick.Vertical;
        move = new Vector3(x, 0, z) * speed * Time.deltaTime;

        if (move.magnitude > 0 && currentState != CharacterState.Running)
        {
            currentState = CharacterState.Running;
        }
        else if (move.magnitude == 0 && currentState != CharacterState.Idle && isGrounded)
        {
            currentState = CharacterState.Idle;
        }

        rigidBody.MovePosition(transform.position + transform.TransformDirection(move));    
    }

    private void UpdateAnimatorParameters()
    {
        // BlendTree Animator Connections
        animator.SetFloat("Horizontal", x);
        animator.SetFloat("Vertical", z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsGroundedLayer(collision.gameObject))
        {
            isGrounded = true;
            if (currentState == CharacterState.Falling)
            {
                currentState = CharacterState.Idle;
            }
        }
    }

    private void HandleStateChanges()
    {
        if (!isGrounded && rigidBody.velocity.y < 0)
        {
            currentState = CharacterState.Falling;
        }
    }

    private void UpdateCameraFOV()
    {
        if (virtualCamera != null)
        {
            var lensSettings = virtualCamera.m_Lens;
            if (currentState == CharacterState.Running)
            {
                currentPov = lensSettings.FieldOfView;
                lensSettings.FieldOfView = Mathf.Lerp(currentPov, 62, 0.1f);
            }
            else if (currentState == CharacterState.Idle)
            {
                currentPov = lensSettings.FieldOfView;
                lensSettings.FieldOfView = Mathf.Lerp(currentPov, 60, 0.1f);
            }
            else if (currentState == CharacterState.Falling)
            {
                currentPov = lensSettings.FieldOfView;
                lensSettings.FieldOfView = Mathf.Lerp(currentPov, 63, 0.1f);
            }
            virtualCamera.m_Lens = lensSettings;
        }
    }

    private bool IsGroundedLayer(GameObject obj)
    {
        return groundLayer == (groundLayer | (1 << obj.layer));
    }
}
