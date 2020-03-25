using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Log
{

    public GameObject projectile;
    public float fireDelay;
    private float fireDelaySeconds;
    public bool canFire = false;


    public override void Start()
    {
        //base class first
        base.Start();
        //necessary to set the fireDelaySeconds or 2 projectiles will be fired on first iteration
        fireDelaySeconds = fireDelay;
    }
    
    public void CanFire()
    {
        //moved from update as we already call CheckDistance in fixed update of base
        //seperated out to own function for readability

        if (currentState != enemyState.idle)
        {
            fireDelaySeconds -= Time.deltaTime;

            if (fireDelaySeconds <= 0)
            {
                canFire = true;
                fireDelaySeconds = fireDelay;
            }
        }
    }

    public override void CheckDistance()
    {
        //can we fire?
        CanFire();
        


        if (player.activeSelf == true)
        {



            if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius)
            {

                if (currentState == enemyState.idle || currentState == enemyState.walk
            && currentState != enemyState.stagger)
                {
                    ChangeState(enemyState.walk);
                    anim.SetBool("wakeUp", true);
                    if (canFire)
                    {
                        Debug.Log("Turret can fire!");

                        //Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                        //ChangeAnim(temp - transform.position);
                        //myRigidbody.MovePosition(temp);
                        Vector3 tempVector = target.transform.position - transform.position;
                        GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);

                        current.GetComponent<Projectile>().Launch(tempVector);
                        canFire = false;




                    }
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
            {
                ChangeState(enemyState.idle);
                anim.SetBool("wakeUp", false);

            }

        } else
        {
            ChangeState(enemyState.idle);
            anim.SetBool("wakeUp", false);
            myRigidbody.velocity = Vector2.zero;
        }
    }

}
