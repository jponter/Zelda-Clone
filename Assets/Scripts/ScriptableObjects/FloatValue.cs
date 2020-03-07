using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu]

public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{

    public float initialValue;

    
    public float RuntimeValue;

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }

    public void OnAfterDeserialize()
    {
        //throw new System.NotImplementedException();
        RuntimeValue = initialValue;
    }
}
