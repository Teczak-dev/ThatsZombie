using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    
    public bool isMap = false;
    public Camera MapCam;
    public PlayerController PC;
    private int zoom = 200;

    public void setMap()
    {
        if (!isMap)
        {
            isMap = true;
            MapCam.gameObject.SetActive(true);
            PC.Camera.SetActive(false);
            PC.UIPlayer.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            MapCam.gameObject.SetActive(false);
            PC.Camera.SetActive(true);
            PC.UIPlayer.SetActive(true);
            Time.timeScale = 1f;
            isMap = false;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isMap)
        {
            
            if (Input.mouseScrollDelta.y > 0)
            {
                if (zoom == 200) zoom = 100;
                
                if (zoom > 10)
                {

                    if (zoom < 30) zoom -= 2;
                    else zoom -= 15;
                    MapCam.orthographicSize = zoom;
                }
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                if (zoom > 100) zoom = 185;
                if (zoom < 200)
                {
                    if (zoom < 30) zoom += 2;
                    else zoom += 15;
                    MapCam.orthographicSize = zoom;
                }

                if (zoom == 200)
                {
                    gameObject.transform.position = new Vector3(12f, 148.229996f, -77.0999985f);
                }
            }

            if (zoom < 150)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    this.gameObject.transform.position += new Vector3(0, 0, 5);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    gameObject.transform.position -= new Vector3(0, 0, 5);
                }

                if (Input.GetKey(KeyCode.A))
                {
                    gameObject.transform.position -= new Vector3(5, 0, 0);
                }
                
                if (Input.GetKey(KeyCode.D))
                {
                    gameObject.transform.position += new Vector3(5, 0, 0);
                }
            }
        }
    }
}
