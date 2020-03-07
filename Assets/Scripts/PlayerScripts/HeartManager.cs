using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;
    

    // Start is called before the first frame update
    void Start()
    {
        //playerCurrentHealth.RuntimeValue = playerCurrentHealth.initialValue;
        UpdateHearts();
    }


    public void InitHearts()
    {
        if (heartContainers.RuntimeValue > hearts.Length) heartContainers.RuntimeValue = hearts.Length;
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        InitHearts();
        
        float tempHealth = playerCurrentHealth.RuntimeValue/ 2;

        Debug.Log("Update Hearts: tempHealth = " + tempHealth.ToString());

        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            if(i <= tempHealth-1)
            {
                //full hearth
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                //empty
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                //halt
                hearts[i].sprite = halfFullHeart;
            }
        }

    }

    
}
