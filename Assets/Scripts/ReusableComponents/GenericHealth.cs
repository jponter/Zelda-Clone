﻿using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    public FloatValue maxHealth;
    [SerializeField] protected float currentHealth;



    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth.RuntimeValue;
      
        Debug.Log(this.gameObject.name + " health to " + currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        currentHealth = maxHealth.RuntimeValue;
    }


    public virtual void Heal(float amountToHeal)
    {
        currentHealth += amountToHeal;
        if (currentHealth > maxHealth.RuntimeValue)
        {
            currentHealth = maxHealth.RuntimeValue;
        }
    }

    public virtual void FullHeal()
    {
        currentHealth = maxHealth.RuntimeValue;
    }

    public virtual void Damage(float amountToDamage)
    {
        currentHealth -= amountToDamage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            
        }
    }

    public virtual void InstantDeath()
    {
        currentHealth = 0;
    }

}
