using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public ScrollRect scrollViewBP;
    public ScrollRect scrollViewWeapon;
    public GameObject itemPrefab;

    [Header("Panels")]
    public GameObject BackPackPanel;
    public GameObject WeaponPanel;
    public GameObject EQPanel;

    [Header("UI_Description")] 
    public Text nazwa;
    public Text opis;
    public Text nazwaW;
    public Text opisW;

    public Item pustySlot;
    private int InventorySize = 5;
    private int pusteSloty = 5;
    private List<Item> Ekwipunek;
    

    private bool isFirstEQMenOpen = false;
    private bool isWeap = false;
    private bool isInv = true;
    

    private void Start()
    {
        Ekwipunek = new List<Item>();
        for (int i = 0; i < InventorySize; i++)
        {
            Ekwipunek.Add(pustySlot);
        }
        
        InitializeInventory();
    }

    private void Update()
    {
        if (isFirstEQMenOpen)
        {
            nazwa.text = "";
            opis.text = "";
            InitializeInventory();
            ChangeEQM();
        }

        if (isWeap)
        {
            isWeap = false;
            WeaponSelectMenu(1);
        }
    }
    
    public void ChangeEQM()
    {
        if (isFirstEQMenOpen)
        {
            isFirstEQMenOpen=false;
        }
        else
        {
            isFirstEQMenOpen = true;
        }
    }

    public void changePanel(string Menu)
    {
        if (isInv)
        {
            if (Menu == "BP")
            {
                BackPackPanel.SetActive(true);
            }
            else if (Menu == "W")
            {
                WeaponPanel.SetActive(true);
                isWeap = true;
            }

            EQPanel.SetActive(false);
            isInv = false;
            if (Menu == "Main")
            {
                EQPanel.SetActive(true);
                isInv = true;
            }
        }
        else
        {
            BackPackPanel.SetActive(false);
            WeaponPanel.SetActive(false);
            EQPanel.SetActive(true);
            isInv = true;
            isWeap = false;
        }
    }
    

    // ReSharper disable Unity.PerformanceAnalysis
    private void InitializeInventory()
    {
        foreach (Transform child in scrollViewBP.content.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < InventorySize; i++)
        {
            GameObject item = Instantiate(itemPrefab, scrollViewBP.content);
            Button itemButton = item.GetComponentInChildren<Button>();
            
            // Przypisz ID elementu jako nazwę przycisku
            itemButton.name = i.ToString();

            
            Text buttonText = itemButton.GetComponentInChildren<Text>();
            if (Ekwipunek[i] != pustySlot)
            {
                buttonText.text = Ekwipunek[i].name;
            }
            else
            {
                buttonText.text = "Puste";
            }
            
            // Dodaj obsługę kliknięcia przycisku z przekazaniem ID
            itemButton.onClick.AddListener(() => OnItemClick(int.Parse(itemButton.name)));
            
        }
        
    }


    public void UpgradeInventory(int howMuchUpgrade)
    {
        for(int i = 0; i<howMuchUpgrade; i++)
        {
            InventorySize++;
            GameObject item = Instantiate(itemPrefab, scrollViewBP.content);
            Button itemButton = item.GetComponentInChildren<Button>();
            
            // Przypisz ID elementu jako nazwę przycisku
            itemButton.name = (InventorySize - 1).ToString();
            
            // Dodaj obsługę kliknięcia przycisku z przekazaniem ID
            itemButton.onClick.AddListener(() => OnItemClick(int.Parse(itemButton.name)));
        }
    }

    // Obsługa kliknięcia przycisku z przekazaniem ID
    public void OnItemClick(int itemID)
    {
        Debug.Log($"Selected Item ID:  {itemID}");
        for (int i = 0; i < Ekwipunek.Count; i++)
        {
            if ( i == itemID )
            {
                nazwa.text = Ekwipunek[i].name;
                opis.text = Ekwipunek[i].description;
                break;
            }
        }
    }

    public bool AddToInv(Item item)
    {
        if (pusteSloty > 0)
        {
            foreach (var VARIABLE in Ekwipunek)
            {
                if (VARIABLE == pustySlot)
                {
                    Ekwipunek[Ekwipunek.IndexOf(VARIABLE)] = item;
                    
                    pusteSloty--;
                    break;
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }


    // ReSharper disable Unity.PerformanceAnalysis
    public void WeaponSelectMenu(int index)
    {
        if (index == 1 || index == 2 )
        {
            
            foreach (Transform child in scrollViewWeapon.content.transform)
            {
                Destroy(child.gameObject);
            }
            
            for (int i = 0; i < Ekwipunek.Count; i++)
            {
                if (Ekwipunek[i] is Firearm)
                {
                    GameObject item = Instantiate(itemPrefab, scrollViewWeapon.content);
                    Button itemButton = item.GetComponentInChildren<Button>();
                    itemButton.name = i.ToString();

                    Text buttonText = itemButton.GetComponentInChildren<Text>();
                    buttonText.text = Ekwipunek[i].name;

                    itemButton.onClick.AddListener(() => OnWeaponClick(int.Parse(itemButton.name)));
                }
            }
            
            
        }
        
        
        
    }
    
    public void OnWeaponClick(int index)
    {
        for (int i = 0; i < Ekwipunek.Count; i++)
        {
            if ( i == index )
            {
                nazwaW.text = Ekwipunek[i].name;
                opisW.text = Ekwipunek[i].description;
                break;
            }
        }
    }
    
    
}
