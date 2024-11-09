using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Player : Character
{
    /*
    [Header("Player Special Settings")]
    [SerializeField] private Transform groundCheck;
    private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private float currentPov;

    void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Movement();
        GroundCheck();
        UpdateCameraFOV();
    }

    private void Movement()
    {
        // Movement Section
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0, vertical);
        //Movement Animation BlendTree Connections
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (move != Vector3.zero)
        {
            currentState = CharacterState.Running;
        }
        else if (isGrounded)
        {
            currentState = CharacterState.Idle;
        }

        charController.Move(move * Time.deltaTime * Speed);

        // Jump Section
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            currentState = CharacterState.Jumping;
        }

        velocity.y += Gravity * Time.deltaTime;
        charController.Move(velocity * Time.deltaTime);

        // Fall or Jump Selection
        if (velocity.y < 0 && !isGrounded)
        {
            currentState = CharacterState.Falling;
        }
        else if (velocity.y > 0 && !isGrounded)
        {
            currentState = CharacterState.Jumping;
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            if (currentState == CharacterState.Falling)
            {
                currentState = CharacterState.Idle;
            }
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
            virtualCamera.m_Lens = lensSettings;
        }
    }
    */
}
