using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CameraZoomPinch : MonoBehaviour 
{
	public int speed = 1;
	public float MINSCALE = 2.0F;
	public float MAXSCALE = 5.0F;
	public float varianceInDistances = 5.0F;
	private Camera selectedCamera;
	private float touchDelta = 0.0F;
	private Vector2 prevDist = new Vector2(0,0);
	private Vector2 curDist = new Vector2(0,0);
	private float startAngleBetweenTouches = 0.0F;
	private int vertOrHorzOrientation = 0; //this tells if the two fingers to each other are oriented horizontally or vertically, 1 for vertical and -1 for horizontal
	private Vector2 midPoint = new Vector2(0,0); //store and use midpoint to check if fingers exceed a limit defined by midpoint for oriantation of fingers
	
	Vector3 hit_position = Vector3.zero;
	Vector3 current_position = Vector3.zero;
	Vector3 camera_position = Vector3.zero;


	private bool lockCameraMovement = false;
	
	// Use this for initialization
	void Start () 
	{
		selectedCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Pinch to zoom
		if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved) 
		{
			
			midPoint = new Vector2(((Input.GetTouch(0).position.x + Input.GetTouch(1).position.x)/2), ((Input.GetTouch(0).position.y - Input.GetTouch(1).position.y)/2)); //store midpoint from first touches
			curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches
			prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); //difference in previous locations using delta positions
			touchDelta = curDist.magnitude - prevDist.magnitude;
			
			if ((Input.GetTouch(0).position.x - Input.GetTouch(1).position.x) > (Input.GetTouch(0).position.y - Input.GetTouch(1).position.y))
			{
				vertOrHorzOrientation = -1; 
			}
			if ((Input.GetTouch(0).position.x - Input.GetTouch(1).position.x) < (Input.GetTouch(0).position.y - Input.GetTouch(1).position.y))
			{
				vertOrHorzOrientation = 1;
			}
			
			if ((touchDelta < 0)) 
			{
				selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView + (1 * speed),15,90);
			}
			
			
			if ((touchDelta > 0))
				
			{
				selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView - (1 * speed),15,90);
			}
		}  else {
			
			bool getMouseGesture = ( Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) ) && Input.touchCount == 0;
			

			// Moving camera
			if (Input.touchCount == 1 || getMouseGesture && lockCameraMovement == false) {
				if ( Input.GetMouseButtonDown(0) || (Input.touchCount == 1 &&Input.GetTouch(0).phase == TouchPhase.Began) ) {
					if ( getMouseGesture ) {
						hit_position =  Input.mousePosition;
					} else {
						hit_position = Input.GetTouch(0).position;
					}
					
					camera_position = selectedCamera.transform.position;
					
				}
				if ( Input.GetMouseButton(0)|| Input.GetTouch(0).phase == TouchPhase.Moved ) {
					if (getMouseGesture) {
						current_position = Input.mousePosition;
					} else {
						current_position = Input.GetTouch(0).position;
					}
					current_position.z = hit_position.z = camera_position.y;
					Vector3 direction = selectedCamera.ScreenToWorldPoint(current_position) - selectedCamera.ScreenToWorldPoint(hit_position);
					direction = direction * -1;
					
					Vector3 position = camera_position + direction;
					Vector3 bottomLeft = selectedCamera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, -selectedCamera.transform.position.z));
					
					Vector3 newPosition = new Vector3(position.x, 300, position.z);
					selectedCamera.transform.position = newPosition;
				}
			}
		}

		// Detect wich object is clicked
		detectClickedObject(Input.mousePosition);
	}


	private IDraggable draggableComponent = null;
	private Vector3 latestDragCameraPosition;
	private Vector3 latestSelectCameraPosition;
	private bool draggingOccured = false;
	
	void detectClickedObject(Vector3 pos)
	{
		Vector3 pointerPosition = Input.mousePosition;
		bool userFingerUp = Input.GetMouseButtonUp(0);
		bool userFingerDown = Input.GetMouseButtonDown(0);

		RaycastHit hit;
		Ray ray = selectedCamera.ScreenPointToRay(pointerPosition);
		
		if (Physics.Raycast (ray, out hit)) {

			if(userFingerUp)
			{
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