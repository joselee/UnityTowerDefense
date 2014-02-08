using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UserInput : MonoBehaviour {

	private bool lockCameraMovement = false;
	private Vector3 hitPosition = Vector3.zero;
	private Vector3 cameraStartPosition = Vector3.zero;
	private Vector3 cameraMovePosition;
	private float DefaultCameraY = 400;

	private Vector3 lastCameraPosition = Vector3.zero;
	private Vector3 cameraVelocity = Vector3.zero;

	void Awake() {
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, DefaultCameraY,Camera.main.transform.position.z);
	}

	// Update is called once per frame
	void Update () {

		bool userFingerUp = false;
		bool userFingerDown = false;
		bool userFingerPressed = false;
		Vector3 pointerPosition = Vector3.zero;


		if(Input.touchCount == 0){
			userFingerUp = Input.GetMouseButtonUp(0);
			userFingerDown = Input.GetMouseButtonDown(0);
			userFingerPressed = Input.GetMouseButton(0);
			pointerPosition = Input.mousePosition;
		} else {
			if (Input.touchCount == 1){
				userFingerUp = Input.GetTouch(0).phase == TouchPhase.Ended;
				userFingerDown = Input.GetTouch(0).phase == TouchPhase.Began;
				userFingerPressed = Input.GetTouch(0).phase == TouchPhase.Moved;
				pointerPosition = Input.GetTouch(0).position;
			}
		}

		selectAndDrag(pointerPosition, userFingerUp, userFingerDown);
		if(!lockCameraMovement)
		{
			moveCamera(userFingerUp, userFingerDown, userFingerPressed, pointerPosition);
		}
	}

	void moveCamera(bool userFingerUp, bool userFingerDown, bool userFingerPressed, Vector3 pointerPosition)
	{
		if ( userFingerDown ) {
			hitPosition = pointerPosition;
			cameraStartPosition = Camera.main.transform.position;
			cameraVelocity = Vector3.zero;
			rigidbody.velocity = Vector3.zero;
		} 

		if ( userFingerPressed ) {
			cameraVelocity = Vector3.zero;
			rigidbody.velocity = Vector3.zero;

			//current_position.z = hit_position.z = camera_position.y;
			pointerPosition.z = hitPosition.z = cameraStartPosition.y;

			// Calculating camera shift
			Vector3 direction = Camera.main.ScreenToWorldPoint(pointerPosition) - Camera.main.ScreenToWorldPoint(hitPosition);	
			direction *= -1;

			Vector3 calculatedPosition = cameraStartPosition + direction;
			cameraMovePosition = new Vector3(calculatedPosition.x, DefaultCameraY, calculatedPosition.z);

			Camera.main.transform.position = cameraMovePosition;

			cameraVelocity = (Camera.main.transform.position - lastCameraPosition) / Time.deltaTime;
			lastCameraPosition = cameraMovePosition;
		}

		// Stopped moving camera.
		if ( userFingerUp && cameraVelocity != Vector3.zero) {
			print ("adding force: " + cameraVelocity);
			rigidbody.AddForce(cameraVelocity, ForceMode.VelocityChange);
			cameraVelocity = Vector3.zero;
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

			if(userFingerUp )
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
				if (draggableComponent != null && SelectGameObject.GetObjectByIndex(0) == draggableComponent) {
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
