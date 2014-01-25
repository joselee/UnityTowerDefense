using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	GameObject selectedObject;
	string cannonButtonCaption = "Cannon";
	// Use this for initialization
	void Start () {
		
	}


	bool cannonButton(string text)
	{
		return GUI.Button(new Rect(10,Screen.height - 35,100,25), text);
	}

	bool missileButton()
	{
		return GUI.Button(new Rect(120,Screen.height - 35,120,25), "MissileLauncher");
	}


	void OnGUI(){
		GUI.Box (new Rect (0,Screen.height - 45,Screen.width,50), "");
		// Cannon button
		if(cannonButton(cannonButtonCaption)) {
			// Storing the object
			selectedObject = GameObject.Find("Cannon");
			Debug.Log("Cannong selected");
		}

		if(missileButton()) {
			// Storing the object
			selectedObject = GameObject.Find("MissileLauncher");
			Debug.Log("MissileLauncher selected");
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
			}
		}
	}
}
