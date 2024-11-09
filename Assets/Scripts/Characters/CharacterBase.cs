using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public enum CharacterState
    {
        Idle,
        Running,
        Jumping,
        Falling
    }

    public CharacterState currentState = CharacterState.Idle;

    [Header("Base Character Settings")]
    public Rigidbody rigidBody;
    public Animator animator;
    public float speed;
    public bool isGrounded;
    public Vector3 startPos;

        private void Start() 
    {
        startPos = gameObject.transform.position;    
    }

    public void TeleportStartPosition()
    {
        gameObject.transform.position = startPos;
    }
}
