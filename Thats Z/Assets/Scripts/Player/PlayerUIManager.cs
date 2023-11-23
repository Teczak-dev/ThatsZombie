using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{

    public PlayerController pc;
    public GameObject UIPanel;
    
    [Header("Inventory")] 
    public GameObject InvPanel;

    public InventorySystem Isys;
    
    private bool isInv = false;
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
            pc.isPause = true;
            isInv = true;
        }
        else
        {
            UIPanel.SetActive(true);
            InvPanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            isInv = false;
            pc.isPause = false;
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

}
