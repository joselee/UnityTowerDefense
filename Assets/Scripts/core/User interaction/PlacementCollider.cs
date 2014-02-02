using UnityEngine;
using System.Collections;

public class PlacementCollider : MonoBehaviour
{
	void OnTriggerStay (Collider enteringObject)
	{
		if (enteringObject.transform.gameObject.layer == LayerMask.NameToLayer ("PlacementCollider")) {
			Transform parent = gameObject.transform.parent.transform;
			if (parent.gameObject.GetComponent<Building> ().UnitSelected) {
				Material redIndicator = Resources.Load ("SelectedIndicator_Material_Red", typeof(Material)) as Material;
				parent.FindChild ("SelectedIndicator").gameObject.renderer.material = redIndicator;
			}
		}
	}
	
	void OnTriggerExit (Collider exitingObject)
	{
		if (exitingObject.transform.gameObject.layer == LayerMask.NameToLayer ("PlacementCollider")) {
			Transform parent = gameObject.transform.parent.transform;
			if (parent.gameObject.GetComponent<Building> ().UnitSelected) {
				Material greenIndicator = Resources.Load ("SelectedIndicator_Material", typeof(Material)) as Material;
				parent.FindChild ("SelectedIndicator").gameObject.renderer.material = greenIndicator;
			}
		}
	}
}
