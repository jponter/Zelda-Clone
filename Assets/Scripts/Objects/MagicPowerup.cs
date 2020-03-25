using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPowerup : Powerup
{

    public FloatValue playerMagic;
    public float magicValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMagic.RuntimeValue += magicValue;
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
        
    }
}
