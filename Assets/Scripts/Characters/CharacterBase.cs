using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("DeathCounter")]
    [SerializeField] private TextMeshProUGUI deathText;

    private void Start()
    {
        startPos = gameObject.transform.position;
    }

    public void TeleportStartPosition()
    {
        int deathCount = PlayerPrefs.GetInt("deathCount");
        gameObject.transform.position = startPos;
        deathCount++;
        PlayerPrefs.SetInt("deathCount", deathCount);

        deathText.text = deathCount.ToString();
    }
}
