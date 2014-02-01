using UnityEngine;
using System.Collections;

public abstract class Building : SelectableGameObject
{
    void Start()
    {
    }

    void Update()
    {

    }

	public override void OnSelect()
	{
		// First run the original OnSelect, which happens for all selectable objects.
		base.OnSelect ();

		// Here, we can add Building specific OnSelect code.
	}
	
	public override void OnDeselect()
	{
		base.OnDeselect ();

	}
}
