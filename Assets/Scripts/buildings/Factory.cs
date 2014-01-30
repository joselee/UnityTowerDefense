using UnityEngine;
using System.Collections;

public class Factory : Building
{
    private string factorySpecificProperty = "I am a factory!";

    void Start()
    {
        this.SomeCommonProperty = "Base property set from child.";
        this.SomeBaseMethod(factorySpecificProperty);
    }

    void Update()
    {
    }
}
