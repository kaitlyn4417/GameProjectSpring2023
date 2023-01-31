using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotBehavior : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;
    public Vector3 currentPosition;
	public float creaturePosOffset;
	public float force;
    public float groundBoundary;
	public GameObject slingshot;

	public GameObject creaturePrefab;
	Rigidbody creature;
	Collider creatureCollider;
	

    public float maxLength;
    bool isMouseDown;
    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);
		
		InstantiateCreature();
    }

	void InstantiateCreature()
	{
		creature = Instantiate(creaturePrefab).GetComponent<Rigidbody>();
		creatureCollider = creature.GetComponent<Collider>();
		creatureCollider.enabled = false;
		creature.isKinematic = true;
	}


    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);
            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition);

			if (creatureCollider)
			{
				creatureCollider.enabled = true;
			}
        }
        else
        {
            ResetStrips();
        }
        
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
		Shoot();
    }

	void Shoot()
	{
		creature.isKinematic = false;
		Vector3 creatureForce = (currentPosition - center.position) * force * -1;
		creature.velocity = creatureForce;
		creature = null;
		creatureCollider = null;	
		//add waitForSeconds thing before freezing position
		//creature.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
		Invoke("InstantiateCreature", 2); 
	}

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);
		
		if (creature)
		{
			Vector3 dir = position - center.position;
			creature.transform.position = position + dir.normalized * creaturePosOffset;
			creature.transform.right = -dir.normalized;
		}
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, groundBoundary, 1000);
        return vector;
    }
}
