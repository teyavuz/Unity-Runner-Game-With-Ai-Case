using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    


    [Header("Main Char Sc")]
    public CharacterController charController;
    
    public float Speed;
    public float JumpHeight;
    public float Gravity;
    protected Vector3 velocity;
    protected bool isGrounded;
    
}
