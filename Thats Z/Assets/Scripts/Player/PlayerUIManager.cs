using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{

    public GameObject UIPanel;
    
    [Header("Inventory")] 
    public GameObject InvPanel;

    public InventorySystem Isys;
    
    private bool isInv = false;

    public void ChangeInventoryView()
    {
        if (!isInv)
        {
            UIPanel.SetActive(false);
            InvPanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            Isys.ChangeEQM();
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
        }
    }

}
