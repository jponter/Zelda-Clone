using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{

    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;
    //public float damage;
    [SerializeField] private string otherTag;
    private string thisTag;
 

    private void OnTriggerEnter2D(Collider2D other)
    {

        //thisTag = gameObject.tag;
        
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smash();
        }


        //Todo: remove this at some point it's a hack
        if (string.IsNullOrEmpty(otherTag))
        {
            Debug.Log("otherTag is Null" + this.gameObject.name);

        }
        

            if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
            //|| other.gameObject.CompareTag("Player") && other.isTrigger)
            {

                if (other.gameObject.CompareTag("enemy") && gameObject.CompareTag("enemy"))
                {
                    // we have an a enemy to enemy collision - ignore it
                    return;
                }


                Rigidbody2D hit = other.GetComponentInParent<Rigidbody2D>();
                if (hit != null)
                {

                    Vector3 difference = hit.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    //hit.AddForce(difference, ForceMode2D.Impulse);
                    hit.DOMove(hit.transform.position + difference, knockTime);
                


                    if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                    {
                        hit.GetComponent<Enemy>().currentState = enemyState.stagger;
                        other.GetComponent<Enemy>().Knock(hit, knockTime);

                    }




                    if (other.gameObject.CompareTag("Player"))
                    {
                        if (other.GetComponentInParent<PlayerMovement>().currentState != PlayerState.stagger)
                        {
                            hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                            other.GetComponentInParent<PlayerMovement>().Knock(knockTime);
                        }
                    }



                }
            }
        
    }




}
