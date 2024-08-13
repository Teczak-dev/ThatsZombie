using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{

    [Header("Powiadomienia")] 
    public GameObject fullInv;
    public GameObject craftedItem;
    public Text cITxt;
    private float time = 1f;
    [Header("Reszta")]
    public PlayerController pc;
    public GameObject UIPanel;
    
    [Header("Inventory")] 
    public GameObject InvPanel;

    public InventorySystem Isys;
    
    public bool isInv = false;
    [Header("Map")] public GameObject MapUI;
    
    [Header("DEATH")] 
    public GameObject DeathPanel;
    

    public void ChangeInventoryView()
    {
        Isys.changePanel("Main");
        if (!isInv)
        {
            UIPanel.SetActive(false);
            InvPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            Isys.ChangeEQM();
            //pc.isPause = true;
            isInv = true;
        }
        else
        {
            UIPanel.SetActive(true);
            Isys.ChangeEQM();
            InvPanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            isInv = false;
            //pc.isPause = false;
        }
    }

    public void Death()
    {
        
        DeathPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        pc.isPause = true;
        
    }
    
    
    public void EQFULL()
    {
        fullInv.SetActive(true);
        StartCoroutine(UkryjPowiadomienie());
    }

    // Metoda do wyświetlania powiadomienia o stworzeniu przedmiotu
    public void CreateItem(string nazwaPrzedmiotu, string type)
    {
        
        craftedItem.SetActive(true);
        if(type == "C") cITxt.text = "Created " + nazwaPrzedmiotu+"!";
        else if(type == "P") cITxt.text = "picked up " + nazwaPrzedmiotu+"!";
        StartCoroutine(UkryjPowiadomienie());
    }

    // Korutyna do automatycznego ukrywania powiadomienia po pewnym czasie
    IEnumerator UkryjPowiadomienie()
    {
        float startRealTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startRealTime < time || time == 0)
        {
            yield return null;
        }

        fullInv.SetActive(false);
        craftedItem.SetActive(false);
    }

}
