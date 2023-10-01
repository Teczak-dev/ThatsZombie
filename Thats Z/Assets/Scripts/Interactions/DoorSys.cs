using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSys : MonoBehaviour
{

    
    public GameObject Door;
    public Animator DoorAnim;
    public string InterActionText = "Press E to open door";
    public GameObject Player;
    private bool isOpen;
    private bool isInRange;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            Player.GetComponent<PlayerController>().SetInterActionText(InterActionText);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isOpen)
                {
                    DoorAnim.Play("OpenDoor");
                    isOpen = true;
                }
                else
                {
                    DoorAnim.Play("CloseDoor");
                    isOpen = false;
                }
                
            }
            
        }
        else
        {
            Player.GetComponent<PlayerController>().SetInterActionText(" ");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Player.tag)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Player.tag)
        {
            isInRange = false;
        }
    }
}
