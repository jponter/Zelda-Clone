using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    private bool isPaused;

    public GameObject pausePanel;
    public GameObject inventoryPanel;

    public bool usingPausePanel;

    public string mainMenu;

    public GameObject MainHUD;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        usingPausePanel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause"))
        {
            ChangePause();
        }
    }


    public void ChangePause()
    {
        

        Debug.Log("Pause button pressed");
        isPaused = !isPaused;
        if (isPaused)
        {
            MainHUD.SetActive(false);
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            usingPausePanel = true;
        }
        else
        {
            pausePanel.SetActive(false);
            inventoryPanel.SetActive(false);
            MainHUD.SetActive(true);
            Time.timeScale = 1.0f;
            
        }
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1.0f;
        MainHUD.SetActive(true);
        SceneManager.LoadScene(mainMenu);
    }

    public void SwitchPanels()
    {
        usingPausePanel = !usingPausePanel;
        if (usingPausePanel)
        {
            pausePanel.SetActive(true);
            inventoryPanel.SetActive(false);
        }
        else
        {
            inventoryPanel.SetActive(true);
            pausePanel.SetActive(false);
        }
    }


}
