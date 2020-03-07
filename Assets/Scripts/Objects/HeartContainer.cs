using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : Powerup
{

    public FloatValue heartContainers;
    public FloatValue playerHealth;

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {

            //one more heart container
            heartContainers.RuntimeValue += 1;
            //full health
            playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2;
            //raise the signal to update the UI
            powerupSignal.Raise();
            //destroy me
            Destroy(this.gameObject);

        }
        
    }

}
