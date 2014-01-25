using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public float Speed;
	public float Range;
	//public GameObject ExplosionEffect;
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
			Debug.Log("Traveled past max range");
			explode();
		}

		if(this.Target)
		{
			transform.LookAt(this.Target);
		}
		else 
		{
			Debug.Log("Lost target.");
			explode();
		}
	}

	public void OnTriggerEnter(Collider enteringObject)
	{
		if(enteringObject.gameObject.tag == "Air_Enemy")
		{
			//In the future, we do damage to the enemy like this:
			// enteringObject.gameObject.dealDamage(this.DamageAmount);

			Debug.Log("Hit enemy!");
			explode ();
		}
	}

	private void explode()
	{
		//Instantiate(this.ExplosionEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
