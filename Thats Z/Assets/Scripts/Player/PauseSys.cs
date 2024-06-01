using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseSys : MonoBehaviour
{

    public Slider pauseSlider;
    public GameObject[] UIPanels;
    public GameObject PausePanel;
    public PlayerController PC;
    private int typ = -1;

    public void Pause(int type)
    {
        int r = Random.Range(1, 100);
        pauseSlider.value = r;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;

        typ = type;
        UIPanels[typ].SetActive(false);
        
        PausePanel.SetActive(true);
        
    }

    public void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        PC.isPause = false;
        
        UIPanels[typ].SetActive(true);
        
        PausePanel.SetActive(false);
        
    }

    public void Back()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void Exit()
    {
        Application.Quit();
    }


}
