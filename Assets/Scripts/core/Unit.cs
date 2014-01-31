using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour, ISelectable {

	public void onSelect()
	{
		Debug.Log("Unit is selected");
	}
	
	public void onDeselect()
	{
		Debug.Log("Unit is deselected");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
