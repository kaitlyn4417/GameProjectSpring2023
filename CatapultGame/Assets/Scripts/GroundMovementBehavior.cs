using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovementBehavior : MonoBehaviour
{
    public GameObject land;
    public float speed;
    public float xPos;

    void Start()
    {
        xPos = 0;
        land.transform.position = new Vector3(xPos, 10, -7);
    }

    void Update()
    {

    }
    IEnumerator Count()
    {
        yield return new WaitForSeconds(1.0f);
    }


    public void Move()
    {
        if (xPos > -20)
        {
            land.transform.Translate(Vector3.left * Time.deltaTime * speed);
            xPos--;
        }
    }
}
