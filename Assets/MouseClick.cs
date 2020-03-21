using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public GameObject selectedObject;
    public LayerMask layerMask;
    void Update()
    {

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0, layerMask);
        {
            if (hitData && Input.GetMouseButtonDown(0))
            {

                selectedObject = hitData.transform.gameObject;
                Debug.Log("mouse click hit " + selectedObject.name);
                Debug.Log("Object transform = " + selectedObject.transform.position);
            }
        }

    }
}
