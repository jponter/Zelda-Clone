using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableListString 
{
    public struct SerialItem
    {
        public string name;
        public int count;
    }


    //public List<string> serializableList = new List<string>();
   public List<SerialItem> serializableList = new List<SerialItem>();
    //public Dictionary<string, int> serializableDictionary = new Dictionary<string, int>();
}
