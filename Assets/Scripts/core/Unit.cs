using UnityEngine;
using System.Collections;

public abstract class Unit : MonoBehaviour, ISelectable {

	void Start () {
	
	}

	void Update () {
	
	}

	public void OnSelect()
	{
		SelectGameObject.HighlightObject (gameObject);
	}
	
	public void OnDeselect()
	{
		SelectGameObject.UnHightlightObject (gameObject);
	}
}
