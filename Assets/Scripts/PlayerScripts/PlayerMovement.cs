using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed = 4.0f;

    private Rigidbody2D myRigidBody;

    private Animator animator;

    private Vector3 change;

    //todo: HEALTH break off health  into own component
    //public FloatValue currentHealth;
    public FloatValue currentMagic;
    //public Signal playerHealthSignal;
    public Signal playerMagicSignal;

    //todo: HEALTH break off playerHit should be in health maybe
    public Signal playerHit;
    //todo: MAGIC playermagic should be part of magic system
    public Signal reduceMagic;

    public VectorValue startingPosition;

    //todo: break off
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;


    //todo: break off into player ability system
    [Header("Projectile Stuff")]
    public GameObject projectile;
    public Item bowItem;

    //todo: break off into own script
    [Header("iFrame Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySrpite;


    // Start is called before the first frame update
    void Start()
    {

        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentState = PlayerState.walk;

        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        transform.position = startingPosition.initialValue;

        //Debug.LogWarning("PlayerMovement Tag is: " + this.gameObject.tag.ToString());
        //if (this.gameObject.CompareTag("Player")) Debug.LogWarning("This game object is tagged with Player!");

        


    }

    // Update is called once per frame
    void Update()
    {
        //is the player in interact

        if (currentState == PlayerState.interact)
        {
            return; //just return if we are in interact
        }

        //change = Vector3.zero;
        //not required input resets the axis.

        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
        {
            Debug.Log("Attack!");
            //play sound
            //Managers.Audio.PlaySoundFX("sword");
            GameEvents.instance.Sound("sword");
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
            {
            StartCoroutine(SecondAttackCo());
            }
    }

    private void FixedUpdate()
    {
        
    
        if(currentState == PlayerState.walk || currentState == PlayerState.idle)
            {
                UpdateAnimationAndMove();
            }
        
        
    }

    //todo:  move knockback to own script
    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
        /*
        currentHealth.RuntimeValue -= damage;

        //todo: HEALTH
        playerHealthSignal.Raise();

        if (currentHealth.RuntimeValue> 0)
        {
            
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        */
    }
    //todo: move knockback to own script
    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidBody != null)
        {
            //screen shake
            //playerHit.Raise();
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;

        }
    }

    //todo: this should go to its own script
    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while( temp < numberOfFlashes)
        {
            mySrpite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySrpite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }

        triggerCollider.enabled = true;
    }


    private IEnumerator AttackCo()
    {
        Debug.Log("AttackCo()");
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;

        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.33f);

        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator SecondAttackCo()
    {
        Debug.Log("SecondAttackCo()");

        if (playerInventory.CheckForItem(bowItem)) //only fire if we have the BOW
        {


            //animator.SetBool("attacking", true);
            currentState = PlayerState.attack;
            yield return null;
            MakeArrow();
            //animator.SetBool("attacking", false);
            yield return new WaitForSeconds(0.33f);

            if (currentState != PlayerState.interact)
            {
                currentState = PlayerState.walk;
            }
        }
        else { Debug.Log("No Bow!"); }
    }


    //todo: this should be part of the ability system
    private void MakeArrow()
    {
        if (playerInventory.currentMagic > 0)
        {
            Vector2 tmp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(tmp, ChooseArrowDirection());
            currentMagic.RuntimeValue -= arrow.magicCost;
            reduceMagic.Raise();

            
        }
    }


    //todo: this should be part of the ability system
    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }


    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {

            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receiveItem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }


    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            //round out move.x and move.y to give finite values to the animation states (8 states)
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            //Debug.Log(change);
            
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }

    }


    void MoveCharacter()
    {
        //Debug.Log(transform.position + change * speed * Time.deltaTime);
        change.Normalize();
        myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }


}
