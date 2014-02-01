using UnityEngine;
using System.Collections;

public abstract class SelectableGameObject : MonoBehaviour, ISelectable {
	public GameObject SelectedIndicator;

	void Start () {
	
	}

	void Update () {
	
	}

	public virtual void OnSelect()
	{
		// Assigns a material named "Assets/Resources/SelectedIndicator_Material" to the object.
		Material indicatorMaterial = Resources.Load("SelectedIndicator_Material", typeof(Material)) as Material;
		GameObject selectedIndicator = gameObject.transform.Find ("SelectedIndicator").gameObject;
		selectedIndicator.renderer.material = indicatorMaterial;

		Debug.Log (gameObject.name + " selected");
	}
	
	public virtual void OnDeselect()
	{
		Material[] emptyMaterialsList = new Material[0];
		GameObject selectedIndicator = gameObject.transform.Find ("SelectedIndicator").gameObject;
		selectedIndicator.renderer.materials = emptyMaterialsList;
		//Debug.Log (gameObject.name + " deselected");
	}
}
