using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{

    public Transform target;
    public float chaseRadius;
    public float attackRadius;

    protected Animator anim;
    protected Rigidbody2D myRigidbody;

    protected GameObject player;
    public Transform homePosition;

    // Start is called before the first frame update
    void Start()
    {
        currentState = enemyState.idle;

        player = GameObject.FindWithTag("Player");
        //target = GameObject.FindWithTag("Player").transform;

        target = player.transform;

        
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("wakeUp", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }


    public virtual void CheckDistance()
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

 


    protected void ChangeState(enemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }


    public void ChangeAnim(Vector2 direction)
    {
        direction = direction.normalized;
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
            
    }

}
