using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour, ISelectable, IDraggable
{
	private bool unitSelected = false;

	void Start()
    {
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
		Debug.Log("Draggin stop");
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
}
