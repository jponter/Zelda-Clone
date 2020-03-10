using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{

    public Item contents;
    public bool isOpen = false;

    public Signal rasieItem;

    public GameObject dialogBox;
    public Text dialogText;

    private Animator anim;

    public Inventory playerInventory;

    public BoolValue storedOpen;
    
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = storedOpen.RuntimeValue;

        if (isOpen)
        {
            anim.SetBool("Openend", true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("attack") && playerInRange)
        {

            if (!isOpen)
            {
                // open the chest
                OpenChest();
            }
            else
            {
                // Chest is already open
                ChestAlreadyOpen();
            }


        }
    }

    public void InterractChest()
    {

    }


    public void OpenChest()
    {
        //dialog window on
        dialogBox.SetActive(true);
        //dialog text = contents text
        dialogText.text = contents.itemDescription;
        //add contents to inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;

        // raise the animation signal

        rasieItem.Raise();
        // set the chest to opened
        isOpen = true;





        // raise the context clue
        Context.Raise();

        anim.SetBool("Openend", true);
        // playerInRange = false;

        storedOpen.RuntimeValue = isOpen;
    }

    public void ChestAlreadyOpen()
    {
        

            // dialog off
            dialogBox.SetActive(false);
            // set the current item to empty
            //playerInventory.currentItem = null;
            // raise the signal to player to stop animating
            rasieItem.Raise();
          
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            Debug.Log("player in range");
            Context.Raise();
            playerInRange = true;

        }

    }

    public  override void OnTriggerExit2D(Collider2D other)
    {


        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Debug.Log("Player exited range");
            playerInRange = false;

            if (!isOpen) { 
            Context.Raise();
            }

            
        }

        
    }
}
