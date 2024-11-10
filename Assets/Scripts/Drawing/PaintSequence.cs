using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintSequence : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera drawingCamera;
    
    [Header("Other Connections")]
    [SerializeField] private Transform finishTpPoint;
    [SerializeField] private Animator boy;
    
    void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameStates.painting)
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            GameManager.Instance.gameState = GameManager.GameStates.painting;
            AudioManager.Instance.PlayMusic(2);
            mainCamera.gameObject.SetActive(false);
            drawingCamera.gameObject.SetActive(true);

            other.gameObject.transform.position = finishTpPoint.position;

            boy.SetTrigger("Dance");
        }
    }
}
