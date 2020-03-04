using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement Stuff")]
    public float moveSpeed;
    public Vector2 directionToMove;
    
    [Header("Lifetime")]
    public float lifetime;
    private float lifetimeSeconds;

    public Rigidbody2D myRigidbody2D;



    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        lifetimeSeconds = lifetime;
    }

    // Update is called once per frame
    void Update()
    {

        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    public void Launch(Vector2 initialVelocity)
    {
        myRigidbody2D.velocity = initialVelocity * moveSpeed;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
            Destroy(this.gameObject);
        
    }
}
