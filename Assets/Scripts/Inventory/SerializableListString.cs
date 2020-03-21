using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerialItem
{
    public string name;
    public int count;
}

[System.Serializable]
public class SerializableListString 
{
  


    //This will be our serializable object 
    public List<SerialItem> serializableList = new List<SerialItem>();
    
}
