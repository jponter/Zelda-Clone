using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : GenericHealth
{
   

    [Header("Death Stuff")]
    public GameObject deathEffect;
    [SerializeField] private float deathEffectDelay = 1f;

    


    

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
        EnemyLoot Loot = GetComponent<EnemyLoot>();
        if (Loot)
        {
            Loot.MakeLoot();
        }
        DeathEffect();
        this.gameObject.SetActive(false);
    }

}
