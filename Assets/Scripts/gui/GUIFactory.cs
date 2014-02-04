using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIFactory  {


	private static List<GUIButton> registeredButtons
		= new List<GUIButton>();	


	// Adding button
	public static void AddButton(GUIButton button)
	{
		registeredButtons.Add (button);
	}

	public static List<GUIButton> GetRegisteredButtons()
	{
		return registeredButtons;
	}
}
