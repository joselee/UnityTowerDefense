using UnityEngine;
using System.Collections;

public class GuiCamera : MonoBehaviour {

	public Camera camera;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.camera.orthographicSize  = Screen.height / 2;
	}
}
