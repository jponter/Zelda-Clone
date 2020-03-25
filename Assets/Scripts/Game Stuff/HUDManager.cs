using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Simple class to hide the HUD by default in editor
 * 
 */

public class HUDManager : MonoBehaviour
{


    public GameObject MainHUD;
    // Start is called before the first frame update
    void Start()
    {
        MainHUD.SetActive(true);
    }

    public void OnApplicationPause()
    {
        DisableHUD();
    }

    public void OnApplicationQuit()
    {
        DisableHUD();
    }

    private void DisableHUD()
    {
        MainHUD.SetActive(false);
        Debug.Log("Setting HUD to disabled");
        Debug.Log("Hud is active?: " + MainHUD.activeInHierarchy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
