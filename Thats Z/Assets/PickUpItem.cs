using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item item;
    public InventorySystem ISys;
    public GameObject Player;
    private bool isInRange = false;
    


    private void Update()
    {
        if (isInRange)
        {
            
            //Player.GetComponent<PlayerController>().SetInterActionText($"Press E to PickUp {item.name}");
            // if (Input.GetKey(KeyCode.E))
            // {
            //     if (ISys.DodajPrzedmiot(item))
            //     {
            //         Destroy(gameObject);
            //     }
            //     else
            //     {
            //         Debug.Log("Błąd lub brak miejsca w EQ");
            //     }
            // }
        }
        else
        {
            Player.GetComponent<PlayerController>().SetInterActionText("");
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

        }
    }
}
