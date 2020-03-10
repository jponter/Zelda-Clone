using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;

    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;


    public void Open(bool clue) //clue lets us decide if we want to update the context clue
    {
        Debug.Log("Door.cs:Open()");
        // sprite renderer off for the door
        doorSprite.enabled = false;
        //set open to true
        open = true;
        // turn off the door's box collider
        physicsCollider.enabled = false;
        //turn off the clueContext and disable it!
        if (clue)
        {
            Context.Raise(); // we need this or the initial ? won't dissapear
            
        }
        clueActive = false;
    }

    public void Close()
    {
        // sprite renderer on for the door
        doorSprite.enabled = true;
        //set open to false
        open = false;
        // turn on the door's box collider
        physicsCollider.enabled = true;
        //turn on the clueContext
        clueActive = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("attack"))
        {
            InterractDoor();
        }
    }

    public void InterractDoor()
    {
        //Debug.Log("Space Pressed ");
        if (playerInRange && thisDoorType == DoorType.key)
        {
            // does the player have a key 
            if (playerInventory.numberOfKeys > 0)
            {
                // remove a key
                playerInventory.numberOfKeys--;
                //if so then call open
                Open(true);

            }

        }
    }

}
