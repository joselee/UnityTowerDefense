using UnityEngine;
using System.Collections;

public class UnitBar : MonoBehaviour {
	
	private static GameObject researchButton;
	private static GameObject buildButton;
	public static bool hideButtons = false;
	public static bool inited = false;
	
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
		
		hideButtons = false;
	}
	
	public static void hide()
	{
		hideButtons = true;
		
	}
	public float transitionDuration = 2.5f;
	
	
	IEnumerator Transition(GameObject o, Vector3 target)
	{
		float t = 0.0f;
		Vector3 startingPos = o.transform.position;
		while (t < 0.2f && o != null)
		{
			t += Time.deltaTime * (Time.timeScale*8);
			o.transform.position = Vector3.Lerp(startingPos, target, t);
			yield return 0;
		}
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	
	// Update is called once per frame
	void Update () {
		
		
		if (UnitBar.researchButton != null && UnitBar.buildButton != null) {
			
			float dest = 0f;
			if ( hideButtons ) {
				dest = -120;
				
			} else {
				dest = 50;
			}
			Vector3 rPos = new Vector3(researchButton.transform.position.x, dest, 1 );
			StartCoroutine(Transition(researchButton, rPos));
			
			Vector3 bPos = new Vector3(buildButton.transform.position.x, dest, 1 );
			StartCoroutine(Transition(buildButton, bPos));
		}
	}
}

