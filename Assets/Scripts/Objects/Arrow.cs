using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody2D;
    public float magicCost;

    [Header("Lifetime")]
    public float lifetime;
    private float lifetimeSeconds;


    // Start is called before the first frame update
    void Start()
    {
        lifetimeSeconds = lifetime;
    }

    public void Update()
    {
        lifetimeSeconds -= Time.deltaTime;
        if (lifetimeSeconds <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("Arrow destroyed by timer");
        }
    }


    public void Setup(Vector2 velocity, Vector3 direciton)
    {
        myRigidbody2D.velocity = velocity.normalized * speed;

        transform.rotation = Quaternion.Euler(direciton);


    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

}
