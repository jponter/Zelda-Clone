using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Log
{

    public Collider2D boundary;


    public override void CheckDistance()
    {
        if (player.activeSelf == true)
        {

            if (Vector3.Distance(target.position, transform.position) <= chaseRadius
                && Vector3.Distance(target.position, transform.position) > attackRadius
                && boundary.bounds.Contains(target.transform.position))
            {

                if (currentState == enemyState.idle || currentState == enemyState.walk
                    && currentState != enemyState.stagger)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

                    ChangeAnim(temp - transform.position);
                    myRigidbody.MovePosition(temp);


                    ChangeState(enemyState.walk);
                    anim.SetBool("wakeUp", true);

                }
            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadius
                || !boundary.bounds.Contains(target.transform.position)) 
            {
                ChangeState(enemyState.idle);
                anim.SetBool("wakeUp", false);

            }
        }
        else
        {
            ChangeState(enemyState.idle);
            anim.SetBool("wakeUp", false);
            myRigidbody.velocity = Vector2.zero;
        }
    }


}
