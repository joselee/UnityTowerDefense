using UnityEngine;
using System.Collections;

public class GuiCamera : MonoBehaviour {

	public Camera camera;

	public GameObject LevelBar;
	public GameObject GuiGroup;
	private GameObject paska;

	private bool display = false;
	// Use this for initialization
	void Start () {
		Debug.Log("Inited GUI");
	
	}
	
	// Update is called once per frame
	void Update () {
		this.camera.orthographicSize  = Screen.height / 2;
		if ( display == false ) {

			float width = 200;
			float height = 57;

			Debug.Log(Screen.width);

			Vector3 position = new Vector3(200, 150, 50);

			GuiGroup.transform.position = new Vector3(0, 0, 0);
			paska = Instantiate(LevelBar, position, Quaternion.identity) as GameObject;
			paska.transform.parent = GuiGroup.transform;
			paska.transform.localScale = new Vector3(width, height, 50);
			paska.gameObject.name ="pukka";




			display = true;
		}

		//Vector3 p = new Vector3(0, -41, 0);
		//paska.transform.position = p;

		//Debug.Log(paska.transform.position.x);
	}
}
