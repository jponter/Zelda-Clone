using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBoundary : MonoBehaviour
{
    public Color color = Color.green;

    private Vector3 v3FrontTopLeft;
    private Vector3 v3FrontTopRight;
    private Vector3 v3FrontBottomLeft;
    private Vector3 v3FrontBottomRight;
    private Vector3 v3BackTopLeft;
    private Vector3 v3BackTopRight;
    private Vector3 v3BackBottomLeft;
    private Vector3 v3BackBottomRight;

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("pushable") && other.isTrigger)
        {

            Debug.Log("Pushable trigger!");
            // we have a pushable object
            Rigidbody2D body = other.GetComponent<Rigidbody2D>();


            if (body)
            {
                Debug.Log("Pushable to Zero " + body.gameObject.name);

                if (body.transform.position.y > v3FrontTopLeft.y - 0.2)
                {
                    // body.MovePosition(new Vector2(body.position.x, v3FrontTopLeft.y ));
                    body.velocity *= -1;
                    //body.angularVelocity = 0;
                    Debug.Log("extent on Y TOP");
                }
                
                
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter " + other.gameObject.name);
    }

    public void OnDrawGizmos()
    {
        BoxCollider2D box = this.GetComponent<BoxCollider2D>();
        Vector3 v3Center = box.bounds.center;
        Vector3 v3Extents = box.bounds.extents;

        v3FrontTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top left corner
        v3FrontTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top right corner
        v3FrontBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom left corner
        v3FrontBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom right corner
        //v3BackTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top left corner
        //v3BackTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top right corner
        //v3BackBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom left corner
        //v3BackBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom right corner

        Debug.DrawLine(v3FrontTopLeft, v3FrontTopRight, color);
        Debug.DrawLine(v3FrontTopRight, v3FrontBottomRight, color);
        Debug.DrawLine(v3FrontBottomRight, v3FrontBottomLeft, color);
        Debug.DrawLine(v3FrontBottomLeft, v3FrontTopLeft, color);

        //Debug.DrawLine(v3BackTopLeft, v3BackTopRight, color);
        //Debug.DrawLine(v3BackTopRight, v3BackBottomRight, color);
        //Debug.DrawLine(v3BackBottomRight, v3BackBottomLeft, color);
        //Debug.DrawLine(v3BackBottomLeft, v3BackTopLeft, color);

        //Debug.DrawLine(v3FrontTopLeft, v3BackTopLeft, color);
        //Debug.DrawLine(v3FrontTopRight, v3BackTopRight, color);
        //Debug.DrawLine(v3FrontBottomRight, v3BackBottomRight, color);
        //Debug.DrawLine(v3FrontBottomLeft, v3BackBottomLeft, color);
    }
}
