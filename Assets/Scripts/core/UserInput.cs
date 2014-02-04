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
	private float DefaultCameraY = 400;

	Vector3 previousFramePosition;
	Vector3 velocity = Vector3.zero;


	// Use this for initialization
	void Start () {
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, DefaultCameraY,Camera.main.transform.position.z);
	}

	// Update is called once per frame
	void Update () {

		bool userFingerUp;
		bool userFingerDown;
		bool userFingerPressed;
		Vector3 pointerPosition;

		bool tracking = false;
		if(Input.touchCount == 0){
			userFingerUp = Input.GetMouseButtonUp(0);
			userFingerDown = Input.GetMouseButtonDown(0);
			userFingerPressed = Input.GetMouseButton(0);
			pointerPosition = Input.mousePosition;
			tracking = true;

			// First check if we're selecting/dragging an object
			selectAndDrag(pointerPosition, userFingerUp, userFingerDown);
			// Move the camera
			if ( lockCameraMovement == false ){
				moveCamera(userFingerUp, userFingerDown, userFingerPressed, pointerPosition);
			}

		} else {
			if (Input.touchCount == 1){
				userFingerUp = Input.GetTouch(0).phase == TouchPhase.Ended;
				userFingerDown = Input.GetTouch(0).phase == TouchPhase.Began;
				userFingerPressed = Input.GetTouch(0).phase == TouchPhase.Moved;
				pointerPosition = Input.GetTouch(0).position;
				tracking = true;

				// First check if we're selecting/dragging an object

				// Move the camera

				moveCamera(userFingerUp, userFingerDown, userFingerPressed, pointerPosition);
				selectAndDrag(pointerPosition, userFingerUp, userFingerDown);

			}
		}

	}

	void moveCamera(bool userFingerUp, bool userFingerDown, bool userFingerPressed, Vector3 pointerPosition)
	{
		if ( userFingerDown && lockCameraMovement == false) {
			// Storing starting point
			hitPosition = pointerPosition;

			stopCameraAnimation = true;
			cameraStartPosition = Camera.main.transform.position;
			velocity = Vector3.zero;
			rigidbody.velocity = velocity;

		}

		if ( userFingerPressed && lockCameraMovement == false) {

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

			//float diffx = cameraStartPosition.x  - Camera.main.transform.position.x  
			//Debug.Log(cameraStartPosition.x + "->" + Camera.main.transform.position.x );

			//rigidbody.AddForce(velocity, ForceMode.VelocityChange);
			//velocity = Vector3.zero;
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
					} else {

					}
				}
				// If we have not move a screen
				if (latestSelectCameraPosition == pointerPosition){
					SelectGameObject.Dispatch(hit.transform.gameObject);
				} else {

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
