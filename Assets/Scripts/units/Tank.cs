using UnityEngine;
using System.Collections;

public class Tank : Building,ISelectable {

	public void onSelect()
	{
		Debug.Log("Tank is selected");
	}

	public void onDeselect()
	{
		Debug.Log("Tank is deselected");
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
