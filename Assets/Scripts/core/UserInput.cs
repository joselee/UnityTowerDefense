using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UserInput : MonoBehaviour
{
	private bool lockCameraMovement = false;
	private Vector3 hitPosition = Vector3.zero;
	private Vector3 cameraStartPosition = Vector3.zero;
	private Vector3 cameraMovePosition = Vector3.zero;
	private float defaultCameraY = 50;

	private Vector3 collisionDirection = Vector3.zero;
	private Vector3 lastCameraPosition = Vector3.zero;
	private Vector3 cameraVelocity = Vector3.zero;
	private List<Vector3> compass = new List<Vector3>();

	private float deadZoneThreshold = 30f;
	private bool withinDeadZone = true;
	private Vector3 deadZoneLeavePosition = Vector3.zero;

	void Awake ()
	{
		Application.targetFrameRate = 60;
	}

	void Start ()
	{
		Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, defaultCameraY, Camera.main.transform.position.z);
		compass.Add(Vector3.left);
		compass.Add(Vector3.right);
		compass.Add(Vector3.forward);
		compass.Add(Vector3.back);
		compass.Add(Vector3.up);
		compass.Add(Vector3.down);
	}

	void Update ()
	{
		bool userFingerUp = false;
		bool userFingerDown = false;
		bool userFingerPressed = false;
		Vector3 pointerPosition = Vector3.zero;


		if (Input.touchCount == 0)
		{
			userFingerUp = Input.GetMouseButtonUp (0);
			userFingerDown = Input.GetMouseButtonDown (0);
			userFingerPressed = Input.GetMouseButton (0);
			pointerPosition = Input.mousePosition;
		}
		else
		{
			if (Input.touchCount == 1)
			{
				userFingerUp = Input.GetTouch (0).phase == TouchPhase.Ended;
				userFingerDown = Input.GetTouch (0).phase == TouchPhase.Began;
				userFingerPressed = Input.GetTouch (0).phase == TouchPhase.Moved;
				pointerPosition = Input.GetTouch (0).position;
			}
		}

		selectAndDrag (pointerPosition, userFingerUp, userFingerDown, userFingerPressed);

		if (!lockCameraMovement)
		{
			moveCamera (userFingerUp, userFingerDown, userFingerPressed, pointerPosition);
		}
	}

	void moveCamera (bool userFingerUp, bool userFingerDown, bool userFingerPressed, Vector3 pointerPosition)
	{
		if (userFingerDown)
		{
			hitPosition = pointerPosition;
			cameraStartPosition = Camera.main.transform.position;
			cameraVelocity = Vector3.zero;
			rigidbody.velocity = Vector3.zero;
		}

		if (userFingerPressed )
		{
			cameraVelocity = Vector3.zero;
			rigidbody.velocity = Vector3.zero;

			// Our camera is rotated 90degrees on the X axis.. so Z axis and Y axis are inverted.
			pointerPosition.z = hitPosition.z = deadZoneLeavePosition.z = cameraStartPosition.y;

			// Add the offset of the deadZone, so that the camera doesn't suddenly jump 30f when it starts moving.
		//	Vector3 deadZoneOffset = deadZoneLeavePosition - hitPosition;
		//	hitPosition += deadZoneOffset;

			// Calculating camera shift
			Vector3 direction = Camera.main.ScreenToWorldPoint (pointerPosition) - Camera.main.ScreenToWorldPoint (hitPosition);
			direction *= -1;

			Vector3 calculatedPosition = cameraStartPosition + direction;
			cameraMovePosition = new Vector3 (calculatedPosition.x, defaultCameraY, calculatedPosition.z);

			// If we're hitting a boundary, dont allow the camera to move any further in that direction.
			//if(isAllowedDirection(direction))
			//{
				Camera.main.transform.position = cameraMovePosition;

				cameraVelocity = (Camera.main.transform.position - lastCameraPosition) / Time.deltaTime;
				lastCameraPosition = cameraMovePosition;
		//	}
		}

		// Stopped moving camera.
		if (userFingerUp && cameraVelocity != Vector3.zero)
		{
			rigidbody.AddForce (cameraVelocity, ForceMode.VelocityChange);
			cameraVelocity = Vector3.zero;
		}
	}

	/*----- Moving a draggable object below ------*/
	private IDraggable draggableComponent = null;
	private Vector3 latestDragCameraPosition;
	private Vector3 latestFingerDownPosition;
	private bool draggingOccured = false;
	private Vector3 terrainPointed;

	void selectAndDrag (Vector3 pointerPosition, bool userFingerUp, bool userFingerDown, bool userFingerPressed)
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (pointerPosition);

		if (Physics.Raycast (ray, out hit))
		{
			if (userFingerDown)
			{
				latestFingerDownPosition = pointerPosition;
				withinDeadZone = true;

				if (hit.transform.gameObject.name == "Terrain")
				{
					terrainPointed = pointerPosition;
				}
				
				
				draggableComponent = DragGameObject.GetDraggable (hit.transform.gameObject);
				if (draggableComponent != null && SelectGameObject.GetObjectByIndex (0) == draggableComponent)
				{
					lockCameraMovement = true;
				}
				else
				{
					lockCameraMovement = false;
				}
			}

			// Once we leave the deadzone, we don't check this anymore until the next touch/mouse down.
			if(userFingerPressed )
			{
				float draggedDistance = Vector3.Distance(latestFingerDownPosition, pointerPosition);
				if(draggedDistance > deadZoneThreshold)
				{
					withinDeadZone = false;
					deadZoneLeavePosition = pointerPosition;
				}
			}

			if (userFingerUp)
			{
				// Deselect all objects
				if (hit.transform.gameObject.name == "Terrain")
				{
					if (withinDeadZone)
					{
						SelectGameObject.DeselectAll ();
					}
				}
				// If we have not move a screen
				if (withinDeadZone)
				{
					SelectGameObject.Dispatch (hit.transform.gameObject);
				}

				if (draggableComponent != null && draggingOccured)
				{
					DragGameObject.DispatchDragStop (draggableComponent);
					draggingOccured = false;
				}

				draggableComponent = null;
				withinDeadZone = true;
			}
		}

		if (draggableComponent != null)
		{
			if (!withinDeadZone)
			{
				lockCameraMovement = DragGameObject.DispatchDrag (draggableComponent, pointerPosition);
				draggingOccured = lockCameraMovement;
			}
		}
		else
		{
			lockCameraMovement = false;
		}
		latestDragCameraPosition = pointerPosition;
	}

	void OnCollisionEnter(Collision collision)
	{
        foreach (ContactPoint contact in collision.contacts) {
        	Vector3 contactDirection = contact.point - transform.position;
        	collisionDirection = getGeneralDirection(contactDirection);
        }
    }
    void OnCollisionExit(Collision collision)
    {
    	collisionDirection = Vector3.zero;
    }

	private Vector3 getGeneralDirection(Vector3 v)
	{
	    float maxDot = -Mathf.Infinity;
	    Vector3 ret = Vector3.zero;
	 
	    foreach( Vector3 direction in compass) { 
	       float t = Vector3.Dot(v, direction);
	       if (t > maxDot) {
	         ret = direction;
	         maxDot = t;
	       }
	    }
	 
	    return ret;
	}

	private bool isAllowedDirection(Vector3 desiredDirection)
	{
		/* When the camera is NOT hitting a boundary, collisionDirection is (0,0,0).
		   When the camera IS hitting a boundary, collisionDirection is like (1,0,0).
		   So, if we multiply and get (0,0,0).. we are not hitting any boundary and safe to move. */

		float x = desiredDirection.x * collisionDirection.x;
		float y = desiredDirection.y * collisionDirection.y;
		float z = desiredDirection.z * collisionDirection.z;
		Vector3 allowed = new Vector3(x,y,z);

		bool rightOrForward = false;
		if(collisionDirection == Vector3.right || collisionDirection == Vector3.forward){
			allowed *= -1;
			rightOrForward = getGeneralDirection(allowed) == collisionDirection;
		}
		
		return (allowed == Vector3.zero) || (getGeneralDirection(allowed) == collisionDirection) || rightOrForward;
	}
}