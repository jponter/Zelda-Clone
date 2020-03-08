using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Inventory/Item")]
[System.Serializable]
public class Item : ScriptableObject
{

    public Sprite itemSprite;
    public string itemDescription;
    public bool isKey;


}
