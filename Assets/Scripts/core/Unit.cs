using UnityEngine;
using System.Collections;

public abstract class Unit : SelectableGameObject {

	void Start () {
	
	}

	void Update () {
	
	}

	public override void OnSelect()
	{
		// First run the original OnSelect, which happens for all selectable objects.
		base.OnSelect ();
		
		// Here, we can add Unit specific OnSelect code.
	}
	
	public override void OnDeselect()
	{
		base.OnDeselect ();
	}
}
