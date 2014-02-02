using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour, ISelectable, IDraggable
{
	private bool unitSelected = false;
	private Vector3 lastValidPosition;
	private bool currentPositionValid;

	void Start()
    {
		lastValidPosition = gameObject.transform.position;
    }

    void Update()
    {

    }


	// Dragging
	public bool OnDragMove(Vector3 position)
	{
		if (unitSelected) {
			transform.position = position;
			return true;
		}
		return false;
	}
	// Dragging stopped
	public void OnDragStop()
	{
		if(!currentPositionValid)
		{
			gameObject.transform.position = lastValidPosition;
		}
		else
		{
			lastValidPosition = gameObject.transform.position;
		}
	}
	// Selecting unit
	public void OnSelect()
	{

		SelectGameObject.HighlightObject (gameObject);
		unitSelected = true;
	}
	// Deselecting unit
	public void OnDeselect()
	{
		SelectGameObject.UnHightlightObject (gameObject);
		unitSelected = false;
	}

	public bool IsSelected
	{
		get{return unitSelected;}
		set{unitSelected = value;}
	}

	public bool CurrentPositionValid 
	{
		get{return currentPositionValid;}
		set{currentPositionValid = value;}
	}
}
