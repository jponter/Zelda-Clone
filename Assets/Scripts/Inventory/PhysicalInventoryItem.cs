using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private InventoryItem thisItem;

    //public  SerializableListString sl = new SerializableListString();
    void AddItemToInventory()
    {
        if (playerInventory && thisItem)
        {

            if (playerInventory.myInventory.Contains(thisItem))
            {
                thisItem.numberHeld++;
                
            }
            else
            {
                playerInventory.myInventory.Add(thisItem);
                thisItem.numberHeld = 1;
                
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            AddItemToInventory();
            Destroy(this.gameObject);
        }
        
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
