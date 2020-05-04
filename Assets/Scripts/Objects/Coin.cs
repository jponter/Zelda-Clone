using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{ 
    public Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        if (powerupSignal != null)
        {
            powerupSignal.Raise();
        }
        else
        {
            Debug.LogError("Coin.cs : powerUpSignal is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {

            playerInventory.coins += 1;
                powerupSignal.Raise();
                Destroy(this.gameObject);




        }
    }
}
