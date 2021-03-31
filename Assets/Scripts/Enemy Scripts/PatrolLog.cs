using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{

    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;

    public float roundingDistance = 0.2f;

    public int waitSeconds = 2;
    private bool waiting;

    // Update is called once per frame
    void Update()
    {
        
    }


    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }

    public override void CheckDistance()
    {
        if (!waiting)
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
                        anim.SetBool("wakeUp", true);

                    }
                }
                else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
                {
                    if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
                    {



                        //ChangeState(enemyState.idle);
                        //anim.SetBool("wakeUp", false);
                        Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);

                        ChangeAnim(temp - transform.position);
                        myRigidbody.MovePosition(temp);
                    }
                    else
                    {
                        ChangeGoal();
                        //StartCoroutine(ChangeGoalWait(waitSeconds));
                    }
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

    private IEnumerator ChangeGoalWait(int seconds)
    {
        waiting = true;
        yield return new WaitForSeconds(seconds);
        waiting = false;
        ChangeGoal();

    }

}
