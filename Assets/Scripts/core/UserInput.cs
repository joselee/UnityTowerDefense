using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UserInput : MonoBehaviour {

	private bool lockCameraMovement = false;
	private Vector3 hitPosition = Vector3.zero;
	private Vector3 cameraStartPosition = Vector3.zero;
	private Vector3 cameraMovePosition;
	private bool stopCameraAnimation = false;
	private float DefaultCameraY = 300;

	Vector3 previousFramePosition;
	Vector3 velocity = Vector3.zero;


	// Use this for initialization
	void Start () {
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, DefaultCameraY,Camera.main.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		bool userFingerUp = Input.GetMouseButtonUp(0);
		bool userFingerDown = Input.GetMouseButtonDown(0);
		bool userFingerPressed = Input.GetMouseButton(0);
		Vector3 pointerPosition = Input.mousePosition;

		// First check if we're selecting/dragging an object
		selectAndDrag(pointerPosition, userFingerUp, userFingerDown);

		// Move the camera
		if ( lockCameraMovement == false ){
			moveCamera(userFingerUp, userFingerDown, userFingerPressed, pointerPosition);
		}
	}

	private bool reachedBoundaries()
	{
		float terrainX = Terrain.activeTerrain.terrainData.size.y;
		float terrainZ = Terrain.activeTerrain.terrainData.size.z;

		float boundCoordX = Camera.main.transform.position.x - Screen.height/2;
		float boundCoordZ = Camera.main.transform.position.z - Screen.width/2;


		bool top = boundCoordX > 0;
		bool bottom = boundCoordX > terrainX;
		bool leftRight = Camera.main.transform.position.z > 222 && Camera.main.transform.position.z < 585;

		return leftRight == false;
	}
	void moveCamera(bool userFingerUp, bool userFingerDown, bool userFingerPressed, Vector3 pointerPosition)
	{
		if ( userFingerDown ) {
			// Storing starting point
			hitPosition = pointerPosition;

			stopCameraAnimation = true;
			cameraStartPosition = Camera.main.transform.position;
			rigidbody.velocity = velocity;
		}

		if ( userFingerPressed ) {

			//current_position.z = hit_position.z = camera_position.y;
			pointerPosition.z = hitPosition.z = cameraStartPosition.y;

			// Calculating camera shift
			Vector3 direction = Camera.main.ScreenToWorldPoint(pointerPosition) - Camera.main.ScreenToWorldPoint(hitPosition);	
			direction *= -1;

			Vector3 calculatedPosition = cameraStartPosition + direction;
			cameraMovePosition = new Vector3(calculatedPosition.x, DefaultCameraY, calculatedPosition.z);

			Camera.main.transform.position = cameraMovePosition;

			// Calculate the velocity of the camera: Distance traveled since the previous frame / time since previous frame.
			Vector3 distance = cameraMovePosition - previousFramePosition;
			velocity = distance/Time.deltaTime;
			previousFramePosition = cameraMovePosition;
		}


		// User finished draggin.. slide the camera to a stop using Rigidbody physics
		if ( userFingerUp ) {
			rigidbody.AddForce(velocity, ForceMode.VelocityChange);
			velocity = Vector3.zero;
		}
	}

	/*----- Moving a draggable object below ------*/
	private IDraggable draggableComponent = null;
	private Vector3 latestDragCameraPosition;
	private Vector3 latestSelectCameraPosition;
	private bool draggingOccured = false;
	private Vector3 terrainPointed;
	
	void selectAndDrag(Vector3 pointerPosition, bool userFingerUp, bool userFingerDown)
	{

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(pointerPosition);
		
		if (Physics.Raycast (ray, out hit)) {

			if(userFingerUp)
			{
				// Deselect all objects
				if ( hit.transform.gameObject.name == "Terrain" ){
					if ( terrainPointed == pointerPosition ){
						SelectGameObject.DeselectAll();
					}
				}
				// If we have not move a screen
				if (latestSelectCameraPosition == pointerPosition){
					SelectGameObject.Dispatch(hit.transform.gameObject);
				}

				lockCameraMovement = false;
				if (draggableComponent != null && draggingOccured){
					DragGameObject.DispatchDragStop(draggableComponent);
					draggingOccured = false;
				}
				draggableComponent = null;

			}
			if(userFingerDown)
			{
				if ( hit.transform.gameObject.name == "Terrain" ){
					terrainPointed = pointerPosition;
				}
				latestSelectCameraPosition = pointerPosition;
				draggableComponent = DragGameObject.GetDraggable(hit.transform.gameObject);
				if (draggableComponent != null) {
					lockCameraMovement = true;
				} else {
					lockCameraMovement = false;
				}
			} 
		}

		if ( draggableComponent != null ) {
			if ( latestDragCameraPosition != pointerPosition){
				lockCameraMovement = DragGameObject.DispatchDrag(draggableComponent, pointerPosition);
				draggingOccured = lockCameraMovement;
			}
		} else {
			lockCameraMovement = false; 
		}
		latestDragCameraPosition = pointerPosition;

	}

}
