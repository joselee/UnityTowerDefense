using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour, ISelectable, IDraggable
{
	public bool UnitSelected = false;

	void Start()
    {
    }

    void Update()
    {

    }


	// Dragging
	public bool OnDragMove(Vector3 position)
	{
		if (UnitSelected) {
			transform.position = position;
			return true;
		}
		return false;
	}
	// Dragging stopped
	public void OnDragStop()
	{
	}
	// Selecting unit
	public void OnSelect()
	{

		SelectGameObject.HighlightObject (gameObject);
		UnitSelected = true;
	}
	// Deselecting unit
	public void OnDeselect()
	{
		SelectGameObject.UnHightlightObject (gameObject);
		UnitSelected = false;
	}
}
