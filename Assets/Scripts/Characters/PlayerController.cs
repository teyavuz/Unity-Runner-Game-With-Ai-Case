using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : CharacterBase
{
    [Header("Player Special Settings")]
    private float x, z;
    private Vector3 move = Vector3.zero;
    [SerializeField] private float jumpSpeed;

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
        Movement();    
    }

    private void Movement()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
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

    private void OnTriggerStay(Collider other) 
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rigidBody.velocity = Vector3.up * jumpSpeed;
            animator.SetTrigger("Jumping");
            currentState = CharacterState.Jumping;
            isGrounded = false;
        }    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsGroundedLayer(collision.gameObject))
        {
            isGrounded = true;
            if (currentState == CharacterState.Jumping || currentState == CharacterState.Falling)
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
            else if (currentState == CharacterState.Jumping)
            {
                currentPov = lensSettings.FieldOfView;
                lensSettings.FieldOfView = Mathf.Lerp(currentPov, 64, 0.1f);
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
