using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTableSYs : MonoBehaviour
{

    public ScrollRect scrollViewCT;
    public GameObject prefabBtn;
    public List<Schemat> Schemats;
    public GameObject CraftingPanel;
    public GameObject Player;
    public InventorySystem InvSys;
    public Text nazwa;
    public Text opis;
    public Button CraftingBtn;

    public void Start()
    {
        
    }
    
    private void InitializeInventory()
    {
        foreach (Transform child in scrollViewCT.content.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < Schemats.Count; i++)
        {
            
                GameObject item = Instantiate(prefabBtn, scrollViewCT.content);
                    
                Button itemButton = item.GetComponentInChildren<Button>();
                itemButton.name = i.ToString();
                Text buttonText = itemButton.GetComponentInChildren<Text>();
                buttonText.text = Schemats[i].name;
                buttonText.fontSize = 30;
                        

                itemButton.onClick.AddListener(() => OnSchemaClick(int.Parse(itemButton.name)));
                    
            
        }
        
    }

    public void OnSchemaClick(int index)
    {
        for (int i = 0; i < Schemats.Count; i++)
        {
            if (i == index)
            {
                nazwa.text = Schemats[i].name;
                opis.text = Schemats[i].description;
                CraftingBtn.gameObject.SetActive(true);

                CraftingBtn.onClick.AddListener(() => Craft(i));
                break;
            }
        }
    }

    public void Craft(int index)
    {
        List<Item> PlayerMiner = new List<Item>();
        List<int> MinIndex = new List<int>();

        for (int i = 0; i < InvSys.Ekwipunek.Count; i++)
        {
            if (InvSys.Ekwipunek[i] is Mineral)
            {
                PlayerMiner.Add(InvSys.Ekwipunek[i]);
                MinIndex.Add(((Mineral)InvSys.Ekwipunek[i]).num);
            }
        }

        int HowMuchIsEnought = 0;
        for (int i = 0; i < PlayerMiner.Count; i++)
        {
            for (int j = 0; j < Schemats[index].NeedMinerals.Count; i++)
            {
                if (PlayerMiner[i].name == Schemats[index].NeedMinerals[j])
                {
                    if (MinIndex[i] >= Schemats[index].numofNeedMinerals[j])
                    {
                        HowMuchIsEnought++;
                    }
                }
            }
        }

        if (HowMuchIsEnought == Schemats[index].NeedMinerals.Count)
        {
            CraftingBtn.interactable = true;
        }
        else
        {
            CraftingBtn.interactable = true;
        }



    }

}
