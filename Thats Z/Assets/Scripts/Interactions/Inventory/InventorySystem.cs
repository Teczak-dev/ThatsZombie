using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class InventorySystem : MonoBehaviour
{
    public ScrollRect scrollViewBP;
    public ScrollRect scrollViewWeapon;
    public GameObject itemPrefab;
    public Equipment Eqp;
    public AmmoDB Ammodb;
    public HungrySys hs;
    public PlayerHealth ph;

    [Header("Panels")]
    public GameObject BackPackPanel;
    public GameObject WeaponPanel;
    public GameObject EQPanel;

    [Header("UI_Description")] 
    public Text nazwa;
    public Text opis;
    public Text nazwaW;
    public Text opisW;
    public Button useBtn;
    public Button SetWeaponBtn;
    public Text SetWeaponTxt;

    public Item pustySlot;
    private int InventorySize = 5;
    private int pusteSloty = 5;
    public List<Item> Ekwipunek;
    

    private bool isFirstEQMenOpen = false;
    private bool isWeap = false;
    private bool isWeap2 = false;
    private bool isWeapShort = false;
    private bool isInv = true;


    private int idSelect = -1;
    

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
        
        if (isWeap2)
        {
            isWeap2 = false;
            WeaponSelectMenu(2);
        }
        if (isWeapShort)
        {
            isWeapShort = false;
            WeaponSelectMenu(3);
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
        nazwaW.text = "";
        opisW.text = "";
        nazwa.text = "";
        opis.text = "";
        if (isInv)
        {
            if (Menu == "BP")
            {
                BackPackPanel.SetActive(true);
                isFirstEQMenOpen = true;
            }
            else if (Menu == "W1")
            {
                WeaponPanel.SetActive(true);
                isWeap = true;
            }
            else if (Menu == "W2")
            {
                WeaponPanel.SetActive(true);
                isWeap2 = true;
            }
            else if (Menu == "W3")
            {
                WeaponPanel.SetActive(true);
                isWeapShort = true;
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
            isWeap2 = false;
            isWeapShort = false;
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
            buttonText.fontSize = 30;
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
                idSelect = itemID;
                nazwa.text = Ekwipunek[i].name;
                opis.text = Ekwipunek[i].description;
                if (Ekwipunek[i] is not Firearm)
                {
                    useBtn.gameObject.SetActive(true);
                }
                else
                {
                    useBtn.gameObject.SetActive(false);
                }
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
                    if (item is Firearm)
                    {
                        bool canAdd = true;
                        int I = 0;
                        for (int i = 0; i < Ekwipunek.Count; i++)
                        {
                            if (Ekwipunek[i] is Firearm && Ekwipunek[i] == item)
                            {
                                canAdd = false;
                                I = i;
                                break;
                            }
                        }

                        if (!canAdd)
                        {
                            Random r = new Random();
                            Ammodb.addAmmo( ((Firearm)Ekwipunek[I]).weaponType, r.Next(0,((Firearm)Ekwipunek[I]).MagSize ) );
                        }
                        else
                        {
                            Ekwipunek[Ekwipunek.IndexOf(VARIABLE)] = item;

                            pusteSloty--;
                            break;
                        }
                    }
                    else
                    {


                        Ekwipunek[Ekwipunek.IndexOf(VARIABLE)] = item;

                        pusteSloty--;
                        break;
                    }
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveFromInv()
    {
        if (idSelect >= 0 && idSelect < Ekwipunek.Count)
        {
            if (Ekwipunek[idSelect] != pustySlot)
            {
                if (Ekwipunek[idSelect] is Firearm)
                {
                    Eqp.RemoveWeapon(idSelect);
                }
                Ekwipunek[idSelect] = pustySlot;
                changePanel("Main");
                idSelect = -1;
                pusteSloty++;


            }
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

            bool isOne = false;
            for (int i = 0; i < Ekwipunek.Count; i++)
            {
                if (Ekwipunek[i] is Firearm && !((Firearm)Ekwipunek[i]).isShort)
                {
                        GameObject item = Instantiate(itemPrefab, scrollViewWeapon.content);
                        
                        Button itemButton = item.GetComponentInChildren<Button>();
                        itemButton.name = i.ToString();
                        Text buttonText = itemButton.GetComponentInChildren<Text>();
                        buttonText.text = Ekwipunek[i].name;
                        buttonText.fontSize = 30;
                        if (Eqp.WeapInd[0] == i)
                        {
                            itemButton.GetComponentInChildren<Image>().color = Color.green; 
                            buttonText.color = Color.white;
                        }
                        if(Eqp.WeapInd[1] == i)
                        {
                            itemButton.GetComponentInChildren<Image>().color = Color.blue; 
                            buttonText.color = Color.white;
                        }


                            itemButton.onClick.AddListener(() => OnWeaponClick(itemButton,int.Parse(itemButton.name),index));
                    
                }
            }
        }
        
        else if (index == 3)
        {
            
            foreach (Transform child in scrollViewWeapon.content.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < Ekwipunek.Count; i++)
            {
                if (Ekwipunek[i] is Firearm && ((Firearm)Ekwipunek[i]).isShort)
                {
                        GameObject item = Instantiate(itemPrefab, scrollViewWeapon.content);
                    
                        Button itemButton = item.GetComponentInChildren<Button>();
                        itemButton.name = i.ToString();
                        Text buttonText = itemButton.GetComponentInChildren<Text>();
                        buttonText.text = Ekwipunek[i].name;
                        buttonText.fontSize = 30;
                        if (Eqp.WeapInd[2] == i )
                        {
                            itemButton.GetComponentInChildren<Image>().color = Color.green;
                            buttonText.color = Color.white;
                        }

                        
                        

                        itemButton.onClick.AddListener(() => OnWeaponClick(itemButton,int.Parse(itemButton.name),index));
                    
                }
            }
            
            
        }
        
    }
    
    public void OnWeaponClick(Button btn,int index, int weapInd)
    {
        
        for (int i = 0; i < Ekwipunek.Count; i++)
        {
            if ( i == index )
            {
                nazwaW.text = Ekwipunek[i].name;
                opisW.text = Ekwipunek[i].description;
                SetWeaponBtn.gameObject.SetActive(true);
                if (Eqp.Weapons[weapInd-1] == Ekwipunek[i])
                {
                    SetWeaponTxt.text = "UnEquip";
                }
                else
                {
                    SetWeaponTxt.text = "Equip";
                }
                SetWeaponBtn.onClick.AddListener(() => SetWeapon(i,weapInd));
                break;
            }
        }
    }

    public void SetWeapon(int index, int weapInd)
    {
       Eqp.SetWeapon(weapInd,Ekwipunek[index],index);
       SetWeaponBtn.onClick.RemoveAllListeners();
       isInv = true;
       changePanel("W"+weapInd.ToString());
       SetWeaponTxt.text = "";
       SetWeaponBtn.gameObject.SetActive(false);
    }
    
    
    //
    //      Przycisk Use
    //

    public void Use()
    {
        if (idSelect>=0 && idSelect < Ekwipunek.Count && Ekwipunek[idSelect] is Food)
        {
            if (hs.hungry < hs.maxhungry)
            {
                
                hs.AddHungry( ((Food)Ekwipunek[idSelect]).Hungry );
                if (hs.hungry > hs.maxhungry) hs.hungry = hs.maxhungry;
                RemoveFromInv();

            }
        }
        else if (idSelect>=0 && idSelect < Ekwipunek.Count && Ekwipunek[idSelect] is FirstAidKit)
        {
            if (ph.health < ph.maxhealth)
            {
                
                ph.AddHealth( ((FirstAidKit)Ekwipunek[idSelect]).Health );
                if (ph.health  > ph.maxhealth) ph.health = ph.maxhealth;
                RemoveFromInv();

            }
        }
    }
    
}
