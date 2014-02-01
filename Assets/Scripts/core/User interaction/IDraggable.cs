using UnityEngine;
using System.Collections;

public interface IDraggable {

	// Use this for initialization
	bool OnDragMove (Vector3 position);
	
	// Update is called once per frame
	void OnDragStop();
}
