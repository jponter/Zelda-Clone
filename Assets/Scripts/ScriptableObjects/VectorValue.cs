using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Runtime Value")]
    public Vector2 initialValue;
    [Header("Default statrting value")]
    public Vector2 defaultValue;

    public void OnAfterDeserialize()
    {
        //throw new System.NotImplementedException();
        initialValue = defaultValue;
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }
}
