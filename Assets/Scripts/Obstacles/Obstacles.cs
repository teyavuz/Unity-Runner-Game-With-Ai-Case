using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacles : MonoBehaviour
{
    public void RotateObstacle(Transform obstacle, float rotateSpeed, float x, float y, float z)
    {
        obstacle.DORotate(obstacle.localRotation.eulerAngles + new Vector3(x, y, z), rotateSpeed);
    }

    public void MoveObstacle(float lastPlace, Transform obstacle, float time)
    {
        obstacle.DOMoveX(lastPlace, time).SetLoops(-1, LoopType.Yoyo);
    }
}
