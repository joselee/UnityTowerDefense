using UnityEngine;
using System.Collections;

public class UnitBar : MonoBehaviour {
	
	private static GameObject researchButton;
	private static GameObject buildButton;
	public static bool hideButtons = false;
	public static bool inited = false;




	// Movement speed in units/sec.
	private static float speed = 1.0f;
	
	// Time when the movement started.
	private static float startTime;
	
	// Total distance between the markers.
	private static float journeyLengthResearch;
	private static float journeyLengthBuild;

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

		journeyLengthResearch =  Vector3.Distance(researchButton.transform.position,
			 new Vector3(researchButton.transform.position.x, 15, 1) );

		journeyLengthBuild =  Vector3.Distance(buildButton.transform.position,
			 new Vector3(buildButton.transform.position.x, 15, 1) );
		
		startTime = Time.time;
		// Calculate the journey length.
		move = true;
		

		hideButtons = false;
	}
	
	public static void hide()
	{
		hideButtons = true;
		
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	
	// Update is called once per frame
	void Update () {
		
		
		if (UnitBar.researchButton != null && UnitBar.buildButton != null) {
			if ( move ){

			}
		}
	}
}

