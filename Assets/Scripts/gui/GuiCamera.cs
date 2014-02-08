using UnityEngine;
using System.Collections;

public class GuiCamera : MonoBehaviour {

	public Camera camera;
	private bool levelBarAttached = false;




	private bool display = false;
	// Use this for initialization
	void Start () {
		Debug.Log("Inited GUI");
	
	}
	
	// Update is called once per frame
	void Update () {
		this.camera.orthographicSize  = Screen.height / 2;
		//this.camera.transform.position = new Vector3(0,0,0);

		if (levelBarAttached == false ){
			UIObject levelBar = new UIObject("gui/levelbar", "bar", 200, 57 );
			levelBar.setPosition(new Vector2(Screen.width - 220, Screen.height - 70));
			UI.attach(levelBar);

			levelBarAttached = true;
		}

		//Vector3 p = new Vector3(0, -41, 0);
		//paska.transform.position = p;

		//Debug.Log(paska.transform.position.x);
	}
}
