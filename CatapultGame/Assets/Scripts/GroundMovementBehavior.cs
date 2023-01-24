using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovementBehavior : MonoBehaviour
{
    public GameObject land;
    public float RotationSpeed;
    void Start()
    {

    }

    void Update()
    {
        land.transform.RotateAround(land.transform.position, transform.right, RotationSpeed);
    }
}
