using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossBodyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moved = false;
    private bool newMove;
    public float moveSpeed;

    enum BodyState
    {
        moveLeftDown,
        moveRight,
        moveLeftUp
    }

    BodyState bodyState;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        bodyState = BodyState.moveLeftDown;
        newMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3( Mathf.Sin(Time.time) / 600f,0, 0);

        if (newMove)
        {
            newMove = false;

            switch (bodyState)
            {
                case BodyState.moveLeftDown:
                    MoveLeftDown();
                    bodyState = BodyState.moveRight;

                    break;
                case BodyState.moveRight:
                    MoveRight();
                    bodyState = BodyState.moveLeftUp;

                    break;
                case BodyState.moveLeftUp:
                    MoveLeftUp();
                    bodyState = BodyState.moveLeftDown;

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
        rb.DOMove(new Vector3(x - 1, y - 2, z), moveSpeed).OnComplete(MoveComplete);

    }

    void MoveRight()
    {

        float x = rb.position.x;
        float y = rb.position.y;
        float z = 0;
        Debug.Log("R : Moving to " + x + " " + y);
        rb.DOMove(new Vector3(x + 2, y, z), moveSpeed).OnComplete(MoveComplete);

    }

    void MoveLeftUp()
    {

        float x = rb.position.x;
        float y = rb.position.y;
        float z = 0;
        Debug.Log("LU : Moving to " + x + " " + y);
        rb.DOMove(new Vector3(x - 1, y + 2, z), moveSpeed).OnComplete(MoveComplete);

    }

    void MoveComplete()
    {
        newMove = true;

    }

}
