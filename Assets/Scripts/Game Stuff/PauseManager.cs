using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    private bool isPaused;

    public GameObject pausePanel;

    public string mainMenu;

    public GameObject MainHUD;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
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
        }
        else
        {
            pausePanel.SetActive(false);
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

}
