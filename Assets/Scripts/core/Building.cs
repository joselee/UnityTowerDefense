using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour, ISelectable
{

	public void onSelect()
	{
		Debug.Log("WarFactory is selected");
	}
	
	public void onDeselect()
	{
		Debug.Log("Building is deselected");
	}

    public string SomeCommonProperty;



    void Start()
    {
    }

    void Update()
    {

    }

    public void SomeBaseMethod(string someParameter)
    {
        Debug.Log("One of my children said: " + someParameter);
        Debug.Log(this.SomeCommonProperty);
    }
}
