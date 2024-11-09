using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonut : Obstacles
{
    [SerializeField] private float moveLastLocation;
    [SerializeField] private float speed;
    void Start()
    {
        MoveObstacle(moveLastLocation,gameObject.transform, speed);
    }
}
