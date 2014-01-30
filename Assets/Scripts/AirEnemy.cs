using UnityEngine;
using System.Collections;

public class AirEnemy : MonoBehaviour {

	public float Health;
	public float MinAltitude = 4.0f;
	public float MaxAltitude = 5.5f;
	public float MinSpeed = 20.0f;
	public float MaxSpeed = 25.0f;

	private float movementSpeed;

	// Use this for initialization
	void Start () {
		Health = 100;
		movementSpeed = Random.Range(MinSpeed, MaxSpeed);


		Vector3 temp = transform.position;
		temp.y = Random.Range(MinAltitude, MaxAltitude);
		transform.position = temp;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
	}

	public void TakeDamage (float damageAmount)
	{
		Health -= damageAmount;
		if(Health <= 0)
		{
			explode ();
			return;
		}
	}

	private void explode()
	{
		Destroy(gameObject);
	}
}
