﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storedValue.RuntimeValue;

        if (active)
        {
            ActivateSwitch();
        }
       


    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // is it the player
        if (other.CompareTag("Player"))
        {

            ActivateSwitch();
        }
    }

    public void ActivateSwitch()
    {
        active = true;
        storedValue.RuntimeValue = active;

        thisDoor.Open(false);
        mySprite.sprite = activeSprite;
    }

}
