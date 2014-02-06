using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIPrinter : MonoBehaviour {
	public GUISkin skin;

	float buttonPosition = 0.0f;
	/*
	void OnGUI()
	{
		GUI.skin = skin;
		List<GUIButton> registeredButtons = GUIFactory.GetRegisteredButtons();
		for (int a = 0; a<registeredButtons.Count; a++){
			GUIButton button = registeredButtons[a];
			GUIProperties properties = button.GetProperties();
			Texture2D texture = (Texture2D) Resources.Load(properties.GetTextureResource(), typeof(Texture2D));
			if ( button.IsHidden() == false ){

				if ( GUI.Button (new Rect ( Screen.width /2 -50, Screen.height-100, 100,100), texture) ) {

					if ( properties.GetClickHandler() != null) {
						IClick handler = properties.GetClickHandler();
						handler.perform();
					}
				}
			} 
		}
	}*/
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
