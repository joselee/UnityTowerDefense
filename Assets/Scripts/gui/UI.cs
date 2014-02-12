using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class UI : MonoBehaviour {

	//private static List<GameObject> createdGameObjects
	//	= new List<GameObject>();

	private static  Dictionary<string,UIObject> instances 
		= new Dictionary<string, UIObject>();

	public Camera camera;
	public static GameObject group;
	public GameObject GuiGroup;

	public static GameObject attach(UIObject o)
	{
		float realPositionX = (Screen.width / 2 - o.getWidth()/2)*-1;
		float realPositionY = (Screen.height / 2 - o.getHeight()/2)*-1;
		Vector2 userPosition = o.getPosition();


		Vector3 correctedPosition 
			= new Vector3( userPosition.x,  userPosition.y, 1);
		Debug.Log(correctedPosition);
		
		GameObject inst 
			= Instantiate(Resources.Load(o.getResourceName()), correctedPosition, Quaternion.identity) as GameObject;
		
		
		inst.transform.parent = group.transform;
		inst.transform.localScale = new Vector3(o.getWidth(), o.getHeight(), 1);
		inst.name = o.getName();
		return inst;
	}

	public static float convertY(float y, float h)
	{
		return (Screen.height / 2 - h/2)*-1 + y;
	}

	public static float convertX(float x, float w)
	{
		return (Screen.height / 2 -w/2)*-1 + x;
	}



	public static bool animateFromBottom(GameObject o, float targetHeigh)
	{
		Vector3 pos = o.transform.position;
		if ( pos.y < targetHeigh ){

			o.transform.position = new Vector3(pos.x, pos.y++, pos.z);
			return false;
		}
		return true;
	}
	// Use this for initialization
	void Start () {
		if (  group == null ){
			group = GuiGroup;
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.camera.orthographicSize  = Screen.height / 2;
		this.camera.transform.position = new Vector3(Screen.width / 2, Screen.height /2 );
		//GuiGroup.transform.localScale = new Vector3(Screen.width, Screen.height, 1);
		GuiGroup.transform.position = new Vector3(Screen.width/2, Screen.height/2, 1);
	}
}
