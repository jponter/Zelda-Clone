using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(fileName ="New Item", menuName ="New Inventory/Items")]
public class InventoryItem : ScriptableObject
{

    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int numberHeld;
    public bool usable;
    public bool unique;
    

    public void OnEnable()
    {
        //ID = GetInstanceID();

        Debug.Log("OnEnable " + itemName + " No: " + numberHeld);
    }

    [System.Obsolete]
    public void OnDisable()
    {
        SetDirty();
    }


    public UnityEvent thisEvent;

    public void Use()
    {
        //Debug.Log("Using Item");

       
            Debug.Log("Using Item " + itemName);
            thisEvent.Invoke();
        
    }

    public void DecreaseAmount(int amountToDecrease)
    {
        //decrease by one and check for less than zero
        numberHeld -= amountToDecrease;
        Debug.Log("Decreasing " + this.itemName.ToString() + " by " + amountToDecrease);
    }

}
