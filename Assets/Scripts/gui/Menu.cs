using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	GameObject selectedObject;
	string cannonButtonCaption = "Cannon";


	public GameObject Cannon;
	public GameObject AirEnemy;
	public GameObject MissileLauncher;
	// Use this for initialization
	void Start () {
		
	}


	bool cannonButton(string text)
	{
		return GUI.Button(new Rect(10,Screen.height - 100,100,100), text);
	}

	bool missileButton()
	{
		return GUI.Button(new Rect(120,Screen.height - 100,120,100), "MissileLauncher");
	}


	void OnGUI(){
		GUI.Box (new Rect (0,Screen.height - 100,Screen.width,100), "");
		// Cannon button
		if(cannonButton(cannonButtonCaption)) {
			// Storing the object
			selectedObject = Cannon;
			Debug.Log("Cannong selected");
		}

		if(missileButton()) {
			// Storing the object
			selectedObject = MissileLauncher;
			Debug.Log("MissileLauncher selected");
		}

		if(GUI.Button(new Rect(300,Screen.height - 100,120,100), "Air Enemy")) {
			// Storing the object
			selectedObject = AirEnemy;
			Debug.Log("AirEnemy selected");
		}
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0) && selectedObject){
			RaycastHit hit;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Plane hPlane = new Plane(Vector3.up, Vector3.zero);
			float distance = 0; 
			if (hPlane.Raycast(ray, out distance)){
				Instantiate(selectedObject,ray.GetPoint(distance), transform.rotation);
				selectedObject = null;


				//Debug.Log(selectedObject.GetComponent<cannon>());

			}
		}
	}
}
