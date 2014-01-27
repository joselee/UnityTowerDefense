using UnityEngine;
using System.Collections;


public class CameraZoomPinch : MonoBehaviour 
{
	public int speed = 1;
	public Camera selectedCamera;
	public float MINSCALE = 2.0F;
	public float MAXSCALE = 5.0F;
	public float varianceInDistances = 5.0F;
	private float touchDelta = 0.0F;
	private Vector2 prevDist = new Vector2(0,0);
	private Vector2 curDist = new Vector2(0,0);
	private float startAngleBetweenTouches = 0.0F;
	private int vertOrHorzOrientation = 0; //this tells if the two fingers to each other are oriented horizontally or vertically, 1 for vertical and -1 for horizontal
	private Vector2 midPoint = new Vector2(0,0); //store and use midpoint to check if fingers exceed a limit defined by midpoint for oriantation of fingers

	Vector3 hit_position = Vector3.zero;
	Vector3 current_position = Vector3.zero;
	Vector3 camera_position = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(1).phase == TouchPhase.Began) 
		{
			
		}
		
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
			
			if ((touchDelta < 0)) //
			{
				selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView + (1 * speed),15,90);
			}
			
			
			if ((touchDelta > 0))
				
			{
				selectedCamera.fieldOfView = Mathf.Clamp(selectedCamera.fieldOfView - (1 * speed),15,90);
			}
		}  else {
			// Moving camera
			if (Input.touchCount == 1) {
				if ( Input.GetTouch(0).phase == TouchPhase.Began ) {
					hit_position = Input.GetTouch(0).position;
					camera_position = selectedCamera.transform.position;
				
				}
				if ( Input.GetTouch(0).phase == TouchPhase.Moved ) {
					current_position = Input.GetTouch(0).position;
					current_position.z = hit_position.z = camera_position.y;
					Vector3 direction = selectedCamera.ScreenToWorldPoint(current_position) - selectedCamera.ScreenToWorldPoint(hit_position);
					direction = direction * -1;

					Vector3 position = camera_position + direction;
					Vector3 bottomLeft = selectedCamera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, -selectedCamera.transform.position.z));

					Vector3 newPosition = new Vector3(position.x, 300, position.z);
					selectedCamera.transform.position = newPosition;
				}
			}
			//Input.GetTouch(0).position.x
		}
	}
}