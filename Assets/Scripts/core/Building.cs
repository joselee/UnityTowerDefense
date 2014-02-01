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

	public bool OnDragMove(Vector3 position)
	{
		if (unitSelected) {
			Debug.Log("Draggin x:" + position.x + ", position z : " + position.z);
			return true;
		}
		return false;
	}

	public void OnDragStop()
	{
		Debug.Log("Draggin stop");
		OnDeselect();
	}

	public void OnSelect()
	{

		SelectGameObject.HighlightObject (gameObject);
		unitSelected = true;
	}
	
	public void OnDeselect()
	{
		SelectGameObject.UnHightlightObject (gameObject);
		unitSelected = false;
	}
}
