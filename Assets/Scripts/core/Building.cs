using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour, ISelectable, IDraggable
{
	private bool unitSelected = false;
	private Vector3 lastValidPosition;
	private bool currentPositionValid = true;

	void Start()
    {
		lastValidPosition = gameObject.transform.position;
    }

    void Update()
    {

    }


	private Vector3 dragStartPosition = Vector3.zero;
	private Vector3 dragStartUnitPosition = Vector3.zero;
	// Dragging
	public bool OnDragMove(Vector3 position)
	{
		if (unitSelected) {
			if ( dragStartPosition == Vector3.zero ){
				dragStartPosition = position;
			}
			if ( dragStartUnitPosition == Vector3.zero)
			{
				dragStartUnitPosition = transform.position;
			}
			transform.position = dragStartUnitPosition + position - dragStartPosition;
			return true;
		}
		return false;
	}

	// Dragging stopped
	public void OnDragStop()
	{
		dragStartPosition = Vector3.zero;
		dragStartUnitPosition = Vector3.zero;
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
