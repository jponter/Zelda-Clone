using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("Starting Event System");
            
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }


        //DontDestroyOnLoad(this);
    }


    public  Action<string,float> onSoundEvent;
    // used to be public Event Action, apparently event not required as no return type


    public void Sound(string sound, float vol = 0.7f)
    {
        onSoundEvent?.Invoke(sound,vol);
        //if (onSound != null)
        //{
        //    onSound(sound);
        //}
    }



}
