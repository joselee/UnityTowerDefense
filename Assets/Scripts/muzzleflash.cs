using UnityEngine;
using System.Collections;

public class muzzleflash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Destroy(gameObject, gameObject.particleSystem.duration + gameObject.particleSystem.startLifetime);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
