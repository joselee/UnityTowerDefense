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
				userFingerMoveHistory = new List<FingerDestination>();
				//Debug.Log(dt/1000);
			}

			Vector3 curPos = Camera.main.transform.position;
			Vector3 shiftDifference = hitPosition - pointerPosition;

			float shiftX = curPos.x;
			float shiftZ = curPos.z;

			Debug.Log(speedZ + ":" + speedX);
			shiftZ += (float)speedZ;
			shiftX += (float)speedX; 


			animationDistance = new Vector3(shiftX, 300,shiftZ);
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
			this.time = UnixTimeStampToDateTime();
		}

		private double UnixTimeStampToDateTime( )
		{
			return (DateTime.Now - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
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
