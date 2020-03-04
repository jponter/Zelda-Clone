using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Powerup
{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float ammountToIncrease;


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {

            //we only want to pick up if not at max health
            
            bool maxHealth = playerHealth.RuntimeValue >= (heartContainers.RuntimeValue * 2f);

            if (!maxHealth)
            {
                playerHealth.RuntimeValue += ammountToIncrease;

                if (playerHealth.RuntimeValue > (heartContainers.RuntimeValue * 2f))
                {
                    playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2f;
                }
                powerupSignal.Raise();
                Destroy(this.gameObject);




            }
            else
            {
                //already at max health so do nothing
                //might want to play an audio cue later so leave this in
            }
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
