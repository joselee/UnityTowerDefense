using UnityEngine;
using System.Collections;

public class MapBoundary : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collisionObject){
		Destroy(collisionObject.gameObject);
	}

	void OnTriggerStay(Collider collisionObject){
		Destroy(collisionObject.gameObject);
	}
}
