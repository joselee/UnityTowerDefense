using UnityEngine;
using System.Collections;
using System;

public abstract class Building : MonoBehaviour, ISelectable, IDraggable
{
	private bool unitSelected = false;
	private Vector3 lastValidPosition;
	private bool currentPositionValid = true;

	private GUIButton showInfoButton;


	// When the button is clicked
	public class ShowInfoMenu : IClick
	{
		public void perform()
		{
			Debug.Log("Clicked");
		}
	};

	void Start()
    {
		// Click handler
		ShowInfoMenu showMenu = new ShowInfoMenu();

		// Properties for buttom
		GUIProperties properties = new GUIProperties();
		properties.SetTexture("build");
		properties.SetClickHandler(showMenu);

		// Creating button
		showInfoButton = new GUIButton(properties);
		// Registering button
		GUIFactory.AddButton(showInfoButton);

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

			float xDiff = position.x - dragStartPosition.x;
			float zDiff = position.z - dragStartPosition.z;


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
		showInfoButton.Show();
		SelectGameObject.HighlightObject (gameObject);
		unitSelected = true;
	}
	// Deselecting unit
	public void OnDeselect()
	{
		showInfoButton.Hide();
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
