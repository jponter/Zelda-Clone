using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Managers.Audio.StopMusic();
        Debug.Log("Main Menu Music");
        Managers.Audio.PlayIntroMusic();
    }

   



    // Update is called once per frame
    void Update()
    {
        
    }
}
