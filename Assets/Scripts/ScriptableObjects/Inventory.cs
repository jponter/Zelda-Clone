using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{

    public Item currentItem;
    public List<Item> items = new List<Item>();

    public int numberOfKeys;
    public int coins;

    public float maxMagic = 10;
    public float currentMagic;


    public void OnEnable()
    {
        currentMagic = maxMagic;
    }

    public void ReduceMagic(float magicCost)
    {
        currentMagic -= magicCost;
    }

    public void AddItem(Item itemToAdd)
    {
        // is the item a key
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

}
