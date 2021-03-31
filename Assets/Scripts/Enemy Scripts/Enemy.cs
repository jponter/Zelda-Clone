using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum enemyState
{
    idle,
    walk,
    attack,
    stagger
}


public class Enemy : MonoBehaviour
{

    public float health;
    //public FloatValue maxHealth;
    public string enemyName;
    //public int baseAttack;
    public float moveSpeed;


    public Vector2 home;
    private bool setUp = true;

    public enemyState currentState;

    [Header("Death Signals")]
    public Signal roomSignal;


    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        //TakeDamage(damage);
    }



    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = enemyState.idle;
            myRigidbody.velocity = Vector2.zero;

        }
    }

    public void OnEnable()
    {
        transform.position = home;
        //Debug.Log("Setting back to home");
        //health = maxHealth.initialValue;
        currentState = enemyState.idle;
    }

    private void Awake()
    {
        //health = maxHealth.initialValue;
        //Debug.Log("Health set to " + health);
        if (setUp)
        {
            setUp = false;
            home = transform.position;
            //Debug.Log("Setting home position of enemy");
        }

    }




    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            //DeathEffect();
            //MakeLoot();

            this.gameObject.SetActive(false);




        }
    }



    public void Die()
    {
        //DeathEffect();
        //MakeLoot();

        this.gameObject.SetActive(false);
        


    }


    //private void DeathEffect()
    //{
    //    if(deathEffect != null)
    //    {
    //        GameObject effect = Instantiate(deathEffect, transform.position , Quaternion.identity);
    //        Destroy(effect, deathEffectDelay);
    //    }
    //}

    //private void MakeLoot()
    //{
    //    if (thisLoot != null)
    //    {
    //        Powerup current = thisLoot.LootPowerup();
    //        if (current != null)
    //        {
    //            Instantiate(current.gameObject, transform.position, Quaternion.identity);
    //        }
    //    }
    //}


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
