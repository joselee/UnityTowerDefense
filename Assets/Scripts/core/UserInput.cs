using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UserInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool userFingerUp = Input.GetMouseButtonUp(0);
		bool userFingerDown = Input.GetMouseButtonDown(0);
		bool userFingerPressed = Input.GetMouseButton(0);
		Vector3 pointerPosition = Input.mousePosition;

		// Move the camera
		moveCamera(userFingerUp, userFingerDown, userFingerPressed, pointerPosition);
	}





	private Vector3 hitPosition = Vector3.zero;
	private Vector3 cameraStartPosition = Vector3.zero;
	private Vector3 cameraMovePosition;
	private Vector3 latestDirection;
	private bool stopCameraAnimation = false;



	private List<FingerDestination> userFingerMoveHistory
		= new List<FingerDestination>();	

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
			cameraMovePosition = new Vector3(calculatedPosition.x, 300, calculatedPosition.z);
			Camera.main.transform.position = cameraMovePosition;
		}


		// User finished draggin
		// Here we have to calculate the animation
		if ( userFingerUp) {
			stopCameraAnimation = false;

			// Calculate speed
			if ( userFingerMoveHistory.Count >= 2) {
				FingerDestination lastCoord = userFingerMoveHistory[userFingerMoveHistory.Count - 1];
				FingerDestination prevCoord = userFingerMoveHistory[userFingerMoveHistory.Count - 2];

				float dx = lastCoord.getX() - prevCoord.getX();
				float dy = lastCoord.getZ() - prevCoord.getZ();

				long dt = lastCoord.getTicks() - prevCoord.getTicks();

				//dx / dt
				Debug.Log(lastCoord.getX() + ":" + prevCoord.getX());
				//Debug.Log(dx + ":" + (dx/dt));
				//Debug.Log(dt/1000);
			}

			Vector3 curPos = Camera.main.transform.position;
			Vector3 shiftDifference = hitPosition - pointerPosition;
			float speed = 50;
			float shiftX = curPos.x;
			float shiftZ = curPos.z;

			if ( Mathf.Abs(shiftDifference.x) > Mathf.Abs(shiftDifference.y) ){
				if ( shiftDifference.x < 0 ){
					shiftZ -= speed;
				} else {
					shiftZ += speed;
				}
			} else {
				if ( shiftDifference.y < 0 ){
					shiftX += speed;
				} else {
					shiftX -= speed;
				}
			}

			animationDistance = new Vector3(shiftX, 300,shiftZ);
			StartCoroutine(SmoothCameraMoveOnTouchEnd());
		}
	}

	private class FingerDestination
	{
		Vector3 pos;
		long time;
		public FingerDestination(Vector3 pos)
		{
			pos = pos;
			time = DateTime.UtcNow.Ticks;
		}

		public float getX()
		{
			return pos.x;
		}

		public float getZ()
		{
			return pos.z;
		}

		public long getTicks()
		{
			return time;
		}
	
	}



	private float transitionDuration = 2.5f;
	private Vector3 animationDistance;
	IEnumerator SmoothCameraMoveOnTouchEnd()
	{
		float t = 0.0f;
		Vector3 startingPos = Camera.main.transform.position;
		while (t < 1.0f && stopCameraAnimation == false)
		{
			t += Time.deltaTime * (Time.timeScale/transitionDuration);
			
			transform.position = Vector3.Lerp(startingPos,animationDistance, t);
			yield return 0;
		}
	}
}
