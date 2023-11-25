using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTLogic : MonoBehaviour
{
    public GameObject Player;
    public CraftingTableSYs CtSys;
    private bool isInRange = false;
    private bool isCraft = false;
    


    private void Update()
    {
        if (isInRange)
        {
            
            Player.GetComponent<PlayerController>().SetInterActionText("Press E to use crafting table");
            if (Input.GetKeyDown(KeyCode.E) && !isCraft)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                CtSys.CraftingPanel.SetActive(true);
                CtSys.InitializeInventory();
                isCraft = true;
            }
            else if (Input.GetKeyDown(KeyCode.E) && isCraft)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                CtSys.CraftingPanel.SetActive(false);
                isCraft = false;
            }
            
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInRange = false;
            Player.GetComponent<PlayerController>().SetInterActionText("");
        }
    }
}
