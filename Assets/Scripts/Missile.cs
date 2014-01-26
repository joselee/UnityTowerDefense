using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public float Speed;
	public float Range;
	public Transform Target;

	
	private float currentDistance;
	
	void Start () {
	}
	
	void Update () {
		// Move the cannonball "Forward" along the Z-axis
		transform.Translate(Vector3.forward * Time.deltaTime * this.Speed);
		
		// Calculate how much distance the cannonball has moved
		currentDistance += Time.deltaTime * this.Speed;
		
		// Destroy the cannonball if it has reached its max range
		if(currentDistance >= this.Range)
		{
			explode();
		}

		if(this.Target)
		{
			transform.LookAt(this.Target);
		}
		else 
		{
			explode();
		}
	}

	public void OnTriggerStay(Collider enteringObject)
	{
		Debug.Log("enemy entered?");
		if(enteringObject.gameObject.tag == "Air_Enemy")
		{
			explode ();
		}
	}

	private void explode()
	{
		Destroy(gameObject);
	}
}
