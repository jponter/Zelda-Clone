using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLog : Enemy
{

    private int currentPathIndex;
    private List<Vector3> pathVectorList;

    [SerializeField] float distanceCheck;
    public Transform target;
  

    protected Animator anim;
    protected Rigidbody2D myRigidbody;

    public GameObject player;
    public Transform homePosition;
    public Vector2 gridsize;
    public bool active;



    public  void Start()
    {
        currentState = enemyState.idle;

        //player = GameObject.FindWithTag("Player");
        //target = GameObject.FindWithTag("Player").transform;

        target = player.transform;


        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("wakeUp", true);
       
    }


    public void Activate()
    {
        Debug.Log("Target = " + target.position);
        //SetTargetPosition(target.position);
        StartCoroutine(SetTargetCo(target));
        active = true;
    }

    public void DeActivate()
    {
        Debug.Log("Deactive PathLog " + gameObject.name);
        active = false;
    }


    IEnumerator SetTargetCo(Transform target)
    {

        
        while (active)
        {
            Pathfinding.Instance.GetGrid().GetXY(target.position, out int x, out int y);
            Debug.Log("CoRoutine setting target " + target.position + " x/y " + x + "/" + y);
            if (x >= 0 && x <= gridsize.x && y >= 0 && y <= gridsize.y)
            {
                SetTargetPosition(target.position);
            }
            else
            {
                Debug.Log("target not in grid, yield");
            }
            
            yield return new WaitForSeconds(2);

        }
    }

    //public Transform target;
    //public float chaseRadius;
    //public float attackRadius;

    //protected Animator anim;
    //protected Rigidbody2D myRigidbody;

    //protected GameObject player;
    //public Transform homePosition;

    // Start is called before the first frame update
    //public virtual void Start()
    //{
    //    currentState = enemyState.idle;

    //    player = GameObject.FindWithTag("Player");
    //    //target = GameObject.FindWithTag("Player").transform;

    //    target = player.transform;


    //    myRigidbody = GetComponent<Rigidbody2D>();
    //    anim = GetComponent<Animator>();
    //    anim.SetBool("wakeUp", true);
    //    //Debug.Log(anim.gameObject.name);
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    if (Application.isPlaying)
    //    {
    //        Gizmos.DrawLine(transform.position, target.position);
    //        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(transform.position, attackRadius);
    //    }



    //}

    public void CheckDistance()
    {
        //Debug.Log("Handle Movement");
        if(pathVectorList!= null)
        {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            Debug.Log("Current Path Index " + currentPathIndex);
            if(Vector3.Distance(transform.position, targetPosition) > distanceCheck)
            {
                Debug.Log("within distance moving towards " + targetPosition);
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                float distanceBefor = Vector3.Distance(transform.position, targetPosition);
                ChangeAnim(moveDir);
                Vector3 temp = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                myRigidbody.MovePosition(temp);

                ChangeState(enemyState.walk);
                anim.SetBool("wakeUp", true);
            }
            else
            {
                currentPathIndex++;
                if(currentPathIndex >= pathVectorList.Count)
                {
                    Debug.Log("Handle Movement: Stop Moving");
                    StopMoving();

                }
            }

        }
        else
        {
            //Debug.Log("Path is Null, sleeping");
            anim.SetBool("wakeUp", false);
        }
    }




    // Update is called once per frame
    public  void FixedUpdate()
    {
        //Debug.Log("PathLog FixedUpdate");
        CheckDistance();

        //get the path to the player

        //move towards the player via the path

    }





    private void StopMoving()
    {
        pathVectorList = null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        Debug.Log("Setting target position");
        currentPathIndex = 0;

        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), target.position);


        if(pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
            Debug.Log("Valid path found");

            if (pathVectorList != null)
            {
                for (int i = 0; i < pathVectorList.Count - 1; i++)
                {
                    //Debug.DrawLine(new Vector3(path[i].x , path[i].y) + parent.position * 1f + Vector3.one, new Vector3(path[i + 1].x , path[i + 1].y ) + parent.position * 1f + Vector3.one, Color.green);
                    Debug.DrawLine(pathVectorList[i],pathVectorList[i+1],Color.red,2f);

                }
            }


        }
    }


    //public virtual void CheckDistance()
    //{
    //    if (player.activeSelf == true)
    //    {

    //        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
    //            && Vector3.Distance(target.position, transform.position) > attackRadius)
    //        {

    //            if (currentState == enemyState.idle || currentState == enemyState.walk
    //                && currentState != enemyState.stagger)
    //            {
    //                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

    //                ChangeAnim(temp - transform.position);
    //                myRigidbody.MovePosition(temp);


    //                ChangeState(enemyState.walk);
    //                anim.SetBool("wakeUp", true);

    //            }
    //        }
    //        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
    //        {
    //            ChangeState(enemyState.idle);
    //            anim.SetBool("wakeUp", false);

    //        }
    //    }
    //    else
    //    {
    //        ChangeState(enemyState.idle);
    //        anim.SetBool("wakeUp", false);
    //        myRigidbody.velocity = Vector2.zero;
    //    }
    //}




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
        anim.SetFloat("moveX", direction.y);

    }

}
