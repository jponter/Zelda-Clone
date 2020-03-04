using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;
    bool contextActive = false;

    public void Enable()
    {

        if (contextActive == false)
        {
            contextActive = true;
            contextClue.SetActive(true);
        }
        else
        {
            contextActive = false;
            contextClue.SetActive(false);
        }
        
    }


}
