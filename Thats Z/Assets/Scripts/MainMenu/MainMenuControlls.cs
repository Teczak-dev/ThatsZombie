using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class MainMenuControlls : MonoBehaviour
{

    [Header("Options")] 
    public Slider FovSlider;
    public Text FovTxt;
    private Resolution[] resolutions;
    public Dropdown ResDropDown;
    [Header("MenuSys")]
    // 0 - menu główne, 1 - start menu, 2 - opt menu
    public GameObject[] Scenes;
    public GameObject[] ScenesLight;
    public GameObject[] ScenesCams;
    public GameObject[] ScenesPanels;
    private char SceneNow = 'M';
    public GameObject CamAnim;
    [Header("StartGame")] public PlayableDirector startCutScene;

    private void Start()
    {
        SetCam(0);
        if (PlayerPrefs.HasKey("FOV"))
        {
            FovTxt.text = PlayerPrefs.GetInt("FOV").ToString();
            FovSlider.value = PlayerPrefs.GetInt("FOV");
        }
        else
        {
            FovTxt.text = "60";
            FovSlider.value = 60;
            PlayerPrefs.SetInt("FOV",60);
        }

        if (PlayerPrefs.HasKey("isFS"))
        {
            if (PlayerPrefs.GetInt("isFS") == 1) Screen.fullScreen = true;
            else Screen.fullScreen = false;
        }else Screen.fullScreen = true;
        
        // resolution setts
        resolutions = Screen.resolutions;
        if (PlayerPrefs.HasKey("Width") && PlayerPrefs.HasKey("Height"))
        {
            Screen.SetResolution(PlayerPrefs.GetInt("Width"), PlayerPrefs.GetInt("Height"),Screen.fullScreen);
        }
        ResDropDown.ClearOptions();

        List<string> options = new List<string>();


        int currentRes = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) currentRes = i;
        }
        
        ResDropDown.AddOptions(options);
        ResDropDown.value = currentRes;
        ResDropDown.RefreshShownValue();

    }

    #region MenuSys
    
    public void SetCam(int id)
    {
        CamAnim.transform.position = ScenesCams[id].transform.position;
        CamAnim.transform.rotation = ScenesCams[id].transform.rotation;
    }

    private void SetMainMenu(bool Show)
    {
        if (Show == false)
        {
            ScenesCams[0].SetActive(false);
            ScenesPanels[0].SetActive(false);
            ScenesLight[0].SetActive(false);
        }
        else
        {
            ScenesCams[0].SetActive(true);
            ScenesPanels[0].SetActive(true);
            ScenesLight[0].SetActive(true);
        }
    }
    
    public void BtnStart()
    {
        SetMainMenu(false);
        Scenes[1].SetActive(true);
        CamAnim.SetActive(true);
        CamAnim.GetComponent<Animator>().Play("StartAnim");//Start anim
        StartCoroutine("StartWaiting");

    }

    public void Opt()
    {
        SetMainMenu(false);
        Scenes[2].SetActive(true);
        CamAnim.SetActive(true);
        CamAnim.GetComponent<Animator>().Play("OptionAnim");//Start anim
        StartCoroutine("OptionWaiting");
    }

    public void Back()
    {
        if (SceneNow == 'S')
        {
            ScenesCams[1].SetActive(false);
            Scenes[0].SetActive(true);
            ScenesPanels[1].SetActive(false);
            CamAnim.SetActive(true);
            CamAnim.GetComponent<Animator>().Play("BackStartAnim");//Start anim
            StartCoroutine("Menu1Waiting");
        }
        else
        {
            ScenesCams[2].SetActive(false);
            Scenes[0].SetActive(true);
            ScenesPanels[2].SetActive(false);
            CamAnim.SetActive(true);
            CamAnim.GetComponent<Animator>().Play("BackOptionAnim");//Start anim
            StartCoroutine("Menu2Waiting");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }


    IEnumerator Menu2Waiting()
    {
        ScenesLight[0].SetActive(true);
        ScenesLight[2].SetActive(false);
        yield return new WaitForSeconds(2.5f);
        Scenes[2].SetActive(false);
        CamAnim.SetActive(false);
        ScenesCams[0].SetActive(true);
        ScenesPanels[0].SetActive(true);
        SceneNow = 'M';
    }
    IEnumerator Menu1Waiting()
    {
        ScenesLight[0].SetActive(true);
        ScenesLight[1].SetActive(false);
        yield return new WaitForSeconds(2f);
        Scenes[1].SetActive(false);
        CamAnim.SetActive(false);
        ScenesCams[0].SetActive(true);
        ScenesPanels[0].SetActive(true);
        SceneNow = 'M';
    }


    IEnumerator OptionWaiting()
    {
        ScenesLight[2].SetActive(true);
        ScenesLight[0].SetActive(false);
        yield return new WaitForSeconds(2.5f);
        Scenes[0].SetActive(false);
        CamAnim.SetActive(false);
        ScenesCams[2].SetActive(true);
        ScenesPanels[2].SetActive(true);
        SceneNow = 'O';
    }
    
    IEnumerator StartWaiting()
    {
        ScenesLight[1].SetActive(true);
        ScenesLight[0].SetActive(false);
        yield return new WaitForSeconds(2f);
        Scenes[0].SetActive(false);
        CamAnim.SetActive(false);
        ScenesCams[1].SetActive(true);
        ScenesPanels[1].SetActive(true);
        SceneNow = 'S';
    }
    
    #endregion MenuSys

    #region Setts
    public void ResetSettings()
    {
        // fov Default
        FovTxt.text = "60";
        FovSlider.value = 60;
        PlayerPrefs.SetInt("FOV",60);
        
        //Grafika Default
        QualitySettings.SetQualityLevel(1);
        
        PlayerPrefs.SetInt("isFS",1);
        
        Screen.SetResolution(1920, 1080,Screen.fullScreen);
        PlayerPrefs.SetInt("Width",1920);
        PlayerPrefs.SetInt("Height", 1080);
    }

    public void UpdateFOV()
    {
        FovTxt.text = FovSlider.value.ToString();
        PlayerPrefs.SetInt("FOV",(int)FovSlider.value);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if(isFullscreen)PlayerPrefs.SetInt("isFS", 1);
        else PlayerPrefs.SetInt("isFS", 0);
    }

    public void SetResolution(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height,Screen.fullScreen);
        PlayerPrefs.SetInt("Width",res.width);
        PlayerPrefs.SetInt("Height", res.height);
    }
    
    #endregion Setts

    #region StartGame

    

    public void StartGame()
    {
        ScenesPanels[1].SetActive(false);
        startCutScene.Play();
        StartCoroutine("StartGameW");
    }

    IEnumerator StartGameW()
    {
        yield return new WaitForSeconds(11f);
        SceneManager.LoadScene("Test");
    }
    
    #endregion StartGame
}
