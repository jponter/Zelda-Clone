using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BossHeadMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moved = false;
    private bool newMove;
    public bool reverse;
    public float moveSpeed;

    enum HeadState
    {
        moveLeftDown,
        moveRightUp
    }

    HeadState headState;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        rb = GetComponent<Rigidbody2D>();
        headState = HeadState.moveLeftDown;
        newMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(0, Mathf.Sin(Time.time ) /-80f, 0);
       if (newMove)
        {
            newMove = false;

            switch (headState)
            {
                case HeadState.moveLeftDown:
                    MoveLeftDown();
                    headState = HeadState.moveRightUp;
                    break;
                
                case HeadState.moveRightUp:
                    MoveRightUp();
                    headState = HeadState.moveLeftDown;
                    break;

                default:
                    break;
            }
        }

    }

    void MoveLeftDown()
    {
        
        float x = rb.position.x;
        float y = rb.position.y;
        float z = 0;
        Debug.Log("LD : Moving to " + x + " " + y);
        if (!reverse)
        {
            rb.DOMove(new Vector3(x - 0.5f, y - 0.5f, z), moveSpeed).OnComplete(MoveComplete);
        }
        else
        {
            rb.DOMove(new Vector3(x , y - 1.0f,  z), moveSpeed).OnComplete(MoveComplete);
        }

    }
    void MoveRightUp()
    {
        
        float x = rb.position.x;
        float y = rb.position.y;
        float z = 0;
        Debug.Log("RU: Moving to " + x + " " + y);
        if (!reverse)
        {
            rb.DOMove(new Vector3(x + 0.5f, y + 0.5f, z), moveSpeed).OnComplete(MoveComplete);
        }
        else
        {
            rb.DOMove(new Vector3(x , y + 1.0f , z), moveSpeed).OnComplete(MoveComplete);
        }


    }

    void MoveComplete()
    {
        newMove = true;
        
    }
  
}
