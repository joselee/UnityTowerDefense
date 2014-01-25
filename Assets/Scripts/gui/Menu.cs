using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}


	void OnGUI(){


		GUI.Box (new Rect (0,Screen.height - 45,Screen.width,50), "");
		// Cannon button
		if(GUI.Button(new Rect(10,Screen.height - 35,100,25), "Cannon")) {
			Debug.Log("Clicked");

			GameObject cannon;

			cannon = GameObject.Find("Cannon");
			Vector3 pos = new Vector3(450, 0, 400);
			Instantiate(cannon, pos, transform.rotation);


		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
