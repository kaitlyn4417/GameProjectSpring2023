using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    public float seconds;
    private WaitForSeconds wfsObj;
    private WaitForFixedUpdate wffuObj;
    public GameObject creaturePrefab;
    Rigidbody creature;
    Collider creatureCollider;

    private void Start()
    {
        wfsObj = new WaitForSeconds(seconds);
       // wffuObj = new WaitForFixedUpdate();
    }

   IEnumerator Count()
   {
           yield return new WaitForSeconds(1.0f);
   }

    public void OnShoot()
    {
		creaturePrefab.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        StartCoroutine(Count());
        creaturePrefab.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;

    }
}
