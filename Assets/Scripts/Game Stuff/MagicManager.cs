using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{

    public Slider magicSlider;
    public Inventory playerInventory;
    public FloatValue playerMagic;

    // Start is called before the first frame update
    void Start()
    {
        magicSlider.maxValue = playerInventory.maxMagic;
        magicSlider.value = playerInventory.maxMagic;
        playerMagic.RuntimeValue = playerInventory.maxMagic;
    }

  
    public void AddMagic()
    {
        //magicSlider.value += 1;
        magicSlider.value = playerMagic.RuntimeValue;
        if (magicSlider.value > magicSlider.maxValue)
        {
            magicSlider.value = magicSlider.maxValue;
            playerMagic.RuntimeValue = playerInventory.maxMagic;
            
        }
        
    }

    public void ReduceMagic(float magicCost)
    {
        playerMagic.RuntimeValue -= magicCost;
    }

    public void DecreaseMagic()
    {
        magicSlider.value = playerMagic.RuntimeValue;
        if(magicSlider.value < 0)
        {
            magicSlider.value = 0;
            playerMagic.RuntimeValue = 0;
        }

        //playerInventory.currentMagic = magicSlider.value;
    }


}
