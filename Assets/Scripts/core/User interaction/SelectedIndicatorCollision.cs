using UnityEngine;
using System.Collections;

public class SelectedIndicatorCollision : MonoBehaviour {

	//private Material greenIndicator;
	//private Material redIndicator;

	void Start()
	{

	}

	void OnTriggerEnter(Collider enteringObject)
	{
		Transform indicatorTransform = enteringObject.transform.FindChild("SelectedIndicator");
		if(indicatorTransform != null && indicatorTransform.gameObject.layer == LayerMask.NameToLayer("PlacementCollider"))
		{
			Material redIndicator = Resources.Load("SelectedIndicator_Material_Red", typeof(Material)) as Material;
			gameObject.renderer.material = redIndicator;
		}
	}

	void OnTriggerExit(Collider exitingObject)
	{
		Transform indicatorTransform = exitingObject.transform.FindChild ("SelectedIndicator");
		if (indicatorTransform != null && indicatorTransform.gameObject.layer == LayerMask.NameToLayer ("PlacementCollider"))
		{
			Material greenIndicator = Resources.Load("SelectedIndicator_Material", typeof(Material)) as Material;
			gameObject.renderer.material = greenIndicator;
		}
	}
}
