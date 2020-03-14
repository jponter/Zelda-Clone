using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicReaction : MonoBehaviour
{

    public FloatValue playerMagic;
    public Signal magicSignal;

    public void Use(int ammountToIncrease)
    {
        playerMagic.RuntimeValue += ammountToIncrease;
        magicSignal.Raise();
    }
}
