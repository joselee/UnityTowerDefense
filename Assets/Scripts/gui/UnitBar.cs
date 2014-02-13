using UnityEngine;
using System.Collections;

public class UnitBar : MonoBehaviour {
	
	private static GameObject researchButton;
	private static GameObject buildButton;
	public static bool hideButtons = false;
	public static bool inited = false;




	// Movement speed in units/sec.
	private static float speed = 20.0f;
	
	// Time when the movement started.
	private static float startTime;
	private static float destinationPosition;
	private static float destinationSpeed;
	

	private static bool move = false;
	
	public static void show()
	{
		
		if ( researchButton != null ){
			GameObject.Destroy(researchButton);
			Debug.Log("removing buttons");
		}
		if ( buildButton != null ){
			GameObject.Destroy(buildButton);
		}
		
		int bWidth = 75;
		UIObject _researchButton = new UIObject("gui/research-button", "research-button", bWidth, bWidth );
		_researchButton.setPosition(new Vector2(Screen.width/2 - bWidth - 15,-95));
		researchButton = UI.attach(_researchButton);
		
		
		UIObject bButton = new UIObject("gui/build-button", "build-button", bWidth, bWidth );
		bButton.setPosition(new Vector2(Screen.width / 2,-95));
		buildButton = UI.attach(bButton);

		startTime = Time.time;
		// Calculate the journey length.
		destinationPosition = 50;
		destinationSpeed = 10;
		move = true;
	}
	
	public static void hide()
	{
		destinationPosition = -95;
		destinationSpeed = 100;
		move = true;
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	
	// Update is called once per frame
	void Update () {
		
		
		if (UnitBar.researchButton != null && UnitBar.buildButton != null && move == true) {
			Debug.Log(destinationPosition);

			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered/destinationSpeed;
		
			Vector3 destinationResearch = new Vector3(researchButton.transform.position.x, destinationPosition, 1);
			Vector3 destinationBuild = new Vector3(buildButton.transform.position.x, destinationPosition, 1);

			
			researchButton.transform.position 
				= Vector3.Lerp(researchButton.transform.position,destinationResearch, fracJourney);

			buildButton.transform.position 
				= Vector3.Lerp(buildButton.transform.position,  destinationBuild, fracJourney);
			//if ( destinationResearch == researchButton.transform.position ){
		//		move = false;
		//	}
		}
		
	}
}

