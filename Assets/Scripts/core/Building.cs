﻿using UnityEngine;
using System.Collections;
using System;

public abstract class Building : MonoBehaviour, ISelectable, IDraggable
{
	private bool unitSelected = false;
	private Vector3 lastValidPosition;
	private bool currentPositionValid = true;




	private bool upgradeButtonShown = false;
	private GameObject upgradeButton;


	void Start()
    {

		lastValidPosition = gameObject.transform.position;
    }

    void Update()
    {
		if ( upgradeButton) {
			//UI.animateFromBottom(upgradeButton,0);
		}
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
			Debug.Log("SELECTED");
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
		//showInfoButton.Show();
		SelectGameObject.HighlightObject (gameObject);

		unitSelected = true;

		UIObject upButton = new UIObject("gui/build-button", "factory-button", 79, 79 );

		upButton.setPosition(new Vector2(Screen.width / 2 - 79/2,30));
		upgradeButton = UI.attach(upButton);
	}
	// Deselecting unit
	public void OnDeselect()
	{
		//showInfoButton.Hide();
		SelectGameObject.UnHightlightObject (gameObject);
		unitSelected = false;
		if ( upgradeButton ) {

			GameObject.Destroy(upgradeButton);
		}
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
