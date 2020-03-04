using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Signal Context;
    public bool playerInRange;
    public bool clueActive = true;

   

    private  void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && !other.isTrigger )
        {
            Debug.Log("player in range");
            playerInRange = true;
            if (clueActive)
            {
                Context.Raise();
            }

        }

    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            Debug.Log("Player exited range");
            playerInRange = false;
            if (clueActive)
            {
                Context.Raise();
            }
        }

    }
}
