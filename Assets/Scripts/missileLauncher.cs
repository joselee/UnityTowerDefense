using UnityEngine;
using System;
using System.Collections;

public class missileLauncher : MonoBehaviour {

	public GameObject Ammunition;
	public float FireRate = 1f;
	public Transform MissileSpawnPoint;
	public Transform MissileLauncherBody;
	public Transform MissileLauncherHead;
	
	private Transform enemyTarget;
	private float nextFireTime;
	
	void Start () {}
	
	void Update () {
		// Enemy is in range.
		if(enemyTarget)
		{
			MissileLauncherHead.LookAt(enemyTarget.position);
			
			if(Time.time > nextFireTime)
			{
				FireMissile();
			}
		}
	}
	
	void OnTriggerEnter(Collider enteringObject){
		// Automatically by Unity when any object enters the current game gameObject's collider.
		if(enteringObject.gameObject.tag == "Air_Enemy")
		{
			nextFireTime = Time.time;
			enemyTarget = enteringObject.gameObject.transform;
		}
	}
	
	void OnTriggerExit(Collider exitingObject){
		// Automatically by Unity when any object leaves the current gameObject's collider.
		if(exitingObject.gameObject.transform == enemyTarget)
		{
			enemyTarget = null;
		}
	}
	
	void FireMissile(){
		nextFireTime = Time.time + FireRate;

		//Create a missile in the correct position and rotation
		GameObject missileObject = Instantiate(Ammunition, MissileSpawnPoint.position, MissileLauncherHead.rotation) as GameObject;
		//missileObject.GetComponent(Missile).Target = this.enemyTarget;


		Missile missile = (Missile) missileObject.GetComponent<Missile>();
		missile.Target = this.enemyTarget;
	}
}
