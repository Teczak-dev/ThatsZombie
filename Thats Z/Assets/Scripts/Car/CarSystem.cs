using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CarSystem : MonoBehaviour
{
    public GameObject PlayerPanel;
    public GameObject CarPanel;
    
    public GameObject Player;
    public CarController[] CarsControllers;
    public GameObject[] CarsElements;
    public bool isPlayerEnter;
    public int selectedCarId = -1;

    public void InteractionWithCar(int id)
    {
        if (isPlayerEnter)
        {
            
            CarsControllers[selectedCarId].isPlayerIn = false;
            CarsControllers[selectedCarId].enabled = false;
            CarsElements[selectedCarId].SetActive(false);
            CarPanel.SetActive(false);
            
            Player.SetActive(true);
            PlayerPanel.SetActive(true);

            isPlayerEnter = false;

        }
        else
        {
            selectedCarId = id;
            CarsControllers[selectedCarId].isPlayerIn = true;
            CarsControllers[selectedCarId].enabled = true;
            CarsElements[selectedCarId].SetActive(true);
            CarPanel.SetActive(true);
            
            Player.SetActive(false);
            PlayerPanel.SetActive(false);

            isPlayerEnter = true;
        }
    }
    
    
}
