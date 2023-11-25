using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item item;
    public InventorySystem ISys;
    public PlayerController PlayerController;
    private bool isInRange = false;
    


    private void Update()
    {
        if (isInRange)
        {
            
            PlayerController.SetInterActionText("Press E to pickup "+item.name);
             if (Input.GetKey(KeyCode.E))
             {
                 if (ISys.AddToInv(item))
                 {
                     Destroy(gameObject);
                     PlayerController.SetInterActionText("");
                }
                else
                {
                    Debug.Log("Błąd lub brak miejsca w EQ");
                }
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
            PlayerController.SetInterActionText("");
        }
    }
}
