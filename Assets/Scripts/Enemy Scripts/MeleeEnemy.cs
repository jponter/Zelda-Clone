using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Log
{
    public override void CheckDistance()
    {
        if (player.activeSelf == true)
        {

            if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                && Vector3.Distance(target.position, transform.position) > attackRadius)
            {

                if (currentState == enemyState.idle || currentState == enemyState.walk
                    && currentState != enemyState.stagger)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                    ChangeAnim(temp - transform.position);
                    myRigidbody.MovePosition(temp);


                    ChangeState(enemyState.walk);
                    //anim.SetBool("wakeUp", true);

                }
            }
            else if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                && Vector3.Distance(target.position, transform.position) <= attackRadius)
            {
                if (currentState == enemyState.walk
                  && currentState != enemyState.stagger)
                {
                    //attack co-routine
                    StartCoroutine(AttackCo());
                }
            }
        }
      
    }


    public IEnumerator AttackCo()
    {
        currentState = enemyState.attack;
        anim.SetBool("attack", true);

        yield return new WaitForSeconds(1.0f);

        currentState = enemyState.walk;
        anim.SetBool("attack", false);
    }

}
