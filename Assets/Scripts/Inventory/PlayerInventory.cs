using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Ineventory", menuName = "New Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{

    public List<InventoryItem> myInventory = new List<InventoryItem>();


    //for serializing
    //public List<string> serializableInventoryItems = new List<string>();

    public SerializableListString SL = new SerializableListString();


}
