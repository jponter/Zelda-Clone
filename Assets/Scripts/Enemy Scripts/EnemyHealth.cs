using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : GenericHealth
{
   

    [Header("Death Stuff")]
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float deathEffectDelay = 1f;

    [Header("Loot Stuff")]
    [SerializeField] private LootTable thisLoot;


    private void MakeLoot()
    {
        if (thisLoot != null)
        {
            Powerup current = thisLoot.LootPowerup();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

  


   

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    void Die()
    {
        MakeLoot();
        DeathEffect();
        this.gameObject.SetActive(false);
    }

}
