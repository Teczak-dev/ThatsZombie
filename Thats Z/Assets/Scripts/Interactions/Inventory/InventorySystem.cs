using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public ScrollRect scrollView;
    public GameObject itemPrefab;

    [Header("UI_Description")] 
    public Text nazwa;
    public Text opis;

    public Item pustySlot;
    private int InventorySize = 5;
    private int pusteSloty = 5;
    private List<Item> Ekwipunek;

    private bool isFirstEQMenOpen = false;

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
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void InitializeInventory()
    {
        foreach (Transform child in scrollView.content.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < InventorySize; i++)
        {
            GameObject item = Instantiate(itemPrefab, scrollView.content);
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
            GameObject item = Instantiate(itemPrefab, scrollView.content);
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
    
}
