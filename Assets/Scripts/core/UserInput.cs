using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UserInput : MonoBehaviour {


	private bool lockCameraMovement = false;
	private Vector3 hitPosition = Vector3.zero;
	private Vector3 cameraStartPosition = Vector3.zero;
	private Vector3 cameraMovePosition;
	private Vector3 latestDirection;
	private bool stopCameraAnimation = false;
	public float CameraSpeed = 1f;
	private float DefaultCameraY = 300;




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

		/*
		Vector3 a = new Vector3(Camera.main.transform.position.x, DefaultCameraY,Camera.main.transform.position.z);
		Ray ray = Camera.main.ScreenPointToRay(a);
		RaycastHit hit;
		if (Physics.Raycast (ray,  out hit)) {
			Debug.DrawLine (ray.origin, hit.point, Color.yellow);
		}*/

		// Move the camera
		if ( lockCameraMovement == false ){
			moveCamera(userFingerUp, userFingerDown, userFingerPressed, pointerPosition);
		}

		selectAndDrag(pointerPosition, userFingerUp, userFingerDown);
	}

	private List<FingerDestination> userFingerMoveHistory
		= new List<FingerDestination>();	


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

		}

		if ( userFingerPressed ) {

			//current_position.z = hit_position.z = camera_position.y;
			pointerPosition.z = hitPosition.z = cameraStartPosition.y;

			// Calculating camera shift
			Vector3 direction = Camera.main.ScreenToWorldPoint(pointerPosition) 
				- Camera.main.ScreenToWorldPoint(hitPosition);	
			direction = direction * -1;



			userFingerMoveHistory.Add(new FingerDestination(direction));

			latestDirection = direction;

			Vector3 calculatedPosition = cameraStartPosition + direction;
			cameraMovePosition = new Vector3(calculatedPosition.x, DefaultCameraY, calculatedPosition.z);


			Camera.main.transform.position = cameraMovePosition;

		}


		// User finished draggin
		// Here we have to calculate the animation
		if ( userFingerUp) {
			stopCameraAnimation = false;

			// Calculate speed
			double speedX = 0;
			double speedZ = 0;
			if ( userFingerMoveHistory.Count >= 2) {
				FingerDestination lastCoord = userFingerMoveHistory[userFingerMoveHistory.Count - 1];
				FingerDestination prevCoord = userFingerMoveHistory[userFingerMoveHistory.Count - 2];

				float dx = lastCoord.getX() - prevCoord.getX();
				float dz = lastCoord.getZ() - prevCoord.getZ();

				double dt = lastCoord.getTicks() - prevCoord.getTicks();
				speedX = Math.Round(dx/dt,3) / 3;
				speedZ = Math.Round(dz/dt,3) / 3;
				// Reset coord list
				userFingerMoveHistory = new List<FingerDestination>();
			}
			
			Vector3 curPos = Camera.main.transform.position;
			Vector3 shiftDifference = hitPosition - pointerPosition;

			float shiftX = curPos.x;
			float shiftZ = curPos.z;


			shiftZ += (float)speedZ;
			shiftX += (float)speedX; 


			animationDistance = new Vector3(shiftX, DefaultCameraY,shiftZ);
			cameraStartPosition = Camera.main.transform.position - animationDistance;
			StartCoroutine(SmoothCameraMoveOnTouchEnd());
		}
	}

	public static float Round(float value, int digits)
	{
		float mult = Mathf.Pow(10.0f, (float)digits);
		return Mathf.Round(value * mult) / mult;
	}

	private class FingerDestination
	{
		Vector3 pos;
		double time;
		public FingerDestination(Vector3 pos)
		{
			this.pos = pos;
			this.time = (DateTime.Now - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
		}
		public float getX()
		{
			return pos.x;
		}
		public float getZ()
		{
			return pos.z;
		}
		public double getTicks()
		{
			return time;
		}
	}



	private float transitionDuration = 2.5f;
	private Vector3 animationDistance;
	IEnumerator SmoothCameraMoveOnTouchEnd()
	{
		float t = 0.1f;

		Vector3 distance = Camera.main.transform.position - animationDistance;
		Vector3 startingPos = Camera.main.transform.position;
		/*
		float speedX = distance.x / cameraStartPosition.x;
		float speedY = distance.x / cameraStartPosition.x;
		float commonSpeed = speedX + speedY;*/
	
		while (t < 1f && stopCameraAnimation == false)
		{


			t += Time.deltaTime * CameraSpeed;//(Time.timeScale/transitionDuration);

			transform.position = Vector3.Lerp(startingPos,animationDistance, t);
			yield return 0;
		}
	}



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
