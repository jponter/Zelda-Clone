using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{

    public bool initialValue;


    public bool RuntimeValue;

    public void OnAfterDeserialize()
    {
        //throw new System.NotImplementedException();
        RuntimeValue = initialValue;
    }
    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
