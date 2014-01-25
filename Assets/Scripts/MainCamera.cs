﻿using UnityEngine;
using System.Collections;


public class MainCamera : MonoBehaviour {
	
	Vector3 hit_position = Vector3.zero;
	Vector3 current_position = Vector3.zero;
	Vector3 camera_position = Vector3.zero;



	float currentZoom = 0;
	
	// Use this for initialization
	void Start () {

		Vector3 initialPosition = new Vector3(496,300, 436);
		transform.position = initialPosition;


	}
	
	void Update(){

		if(Input.GetMouseButtonDown(0)){
			hit_position = Input.mousePosition;
			camera_position = transform.position;
			
		}
		if(Input.GetMouseButton(0)){
			current_position = Input.mousePosition;
			LeftMouseDrag();        
		}

		if(Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if ( currentZoom < 16 ) {
				currentZoom++;
				Vector3 pos = new Vector3(transform.position.x,transform.position.y-currentZoom, transform.position.z);
				Debug.Log(currentZoom);
				transform.position = pos;
			}
		}

		if(Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if ( currentZoom > 0 ) {
				currentZoom--;
				Vector3 pos = new Vector3(transform.position.x,transform.position.y+currentZoom, transform.position.z);
				
				transform.position = pos;
			}
		}
		
		
		


	}
	
	void LeftMouseDrag(){




		// From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
		// with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
		current_position.z = hit_position.z = camera_position.y;
		
		// Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
		// anyways.  
		Vector3 direction = Camera.main.ScreenToWorldPoint(current_position) - Camera.main.ScreenToWorldPoint(hit_position);
		
		// Invert direction to that terrain appears to move with the mouse.
		direction = direction * -1;
		
		Vector3 position = camera_position + direction;



		Vector3 bottomLeft = camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, -transform.position.z));
		//Debug.Log(bottomLeft);


		//Debug.Log("left:" + bottomLeft + " -> x: " + position.x +" y: " + position.z);
		//Debug.Log( Terrain.activeTerrain.transform.position.x );


		if ( position.z > 250.0 && position.z < 545.0 &&  position.x < 674 && position.x > 118){ 
			transform.position = position;
		}
	}
}
