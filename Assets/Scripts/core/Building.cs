using UnityEngine;
using System.Collections;

public abstract class Building : MonoBehaviour
{

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
