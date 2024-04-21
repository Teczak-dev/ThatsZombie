using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSys : MonoBehaviour
{

    
    public GameObject Door;
    public Animator DoorAnim;
    public string InterActionText1 = "Press E to open the door";
    public string InterActionText2 = "Press E to close the door";
    public GameObject Player;
    private bool isOpen;
    private bool isInRange = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if(!isOpen) Player.GetComponent<PlayerController>().SetInterActionText(InterActionText1);
            else Player.GetComponent<PlayerController>().SetInterActionText(InterActionText2);
            
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
                Player.GetComponent<PlayerController>().SetInterActionText(" ");
            }
            
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
            Player.GetComponent<PlayerController>().SetInterActionText("");
        }
    }
}
