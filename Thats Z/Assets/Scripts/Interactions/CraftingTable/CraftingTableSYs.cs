using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTableSYs : MonoBehaviour
{

    public PlayerUIManager pUIm;
    public ScrollRect scrollViewCT;
    public GameObject prefabBtn;
    public List<Schemat> Schemats;
    public GameObject CraftingPanel;
    public InventorySystem InvSys;
    public Text nazwa;
    public Text opis;
    public Button CraftingBtn;
   

    public void InitializeInventory()
    {
        foreach (Transform child in scrollViewCT.content.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < Schemats.Count; i++)
        {
            
            Debug.Log(i);
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
                opis.text = Schemats[i].description+"\nWymagania\n";
                for (int j = 0; j < Schemats[i].NeedMinerals.Count; j++)
                {
                    opis.text += Schemats[i].NeedMinerals[j] + ": " + Schemats[i].numofNeedMinerals[j] + "  |||  ";
                }
                CraftingBtn.gameObject.SetActive(true);

                CraftingBtn.onClick.AddListener(() => Craft(index));
                break;
            }
        }
    }

    public void Craft(int index)
    {
        Debug.Log("Start ()=> Craft();");
        List<Item> PlayerMiner = new List<Item>();
        List<int> MinIndex = new List<int>();

        PlayerMiner = InvSys.GetMinerals();
        MinIndex = InvSys.getMineralsNum();

        int HowMuchIsEnought = 0;
        for (int i = 0; i < PlayerMiner.Count; i++)
        {
            for (int j = 0; j < Schemats[index].NeedMinerals.Count; j++)
            {
                if (PlayerMiner[i].name == Schemats[index].NeedMinerals[j] && MinIndex[i] >= Schemats[index].numofNeedMinerals[j]) HowMuchIsEnought++;
                
            
            }
        }

        if (HowMuchIsEnought == Schemats[index].NeedMinerals.Count)
        {
            if (InvSys.pusteSloty > 0)
            {
                for (int i = 0; i < PlayerMiner.Count; i++)
                {
                    for (int j = 0; j < Schemats[index].NeedMinerals.Count; j++)
                    {
                        if (PlayerMiner[i].name == Schemats[index].NeedMinerals[j] &&
                            MinIndex[i] >= Schemats[index].numofNeedMinerals[j])
                        {

                            MinIndex[i] -= Schemats[index].numofNeedMinerals[j];
                            if (MinIndex[i] <= 0)
                            {
                                PlayerMiner.RemoveAt(i);
                                MinIndex.RemoveAt(i);
                            }

                            InvSys.AddToInv(Schemats[index].ItemCrafted);
                            InvSys.SetMinerals(PlayerMiner,MinIndex);
                            pUIm.CreateItem(Schemats[index].ItemCrafted.name, "C");
                            break;
                        }
                        
            
                    }
                }
            }
            else
            {
                pUIm.EQFULL();
            }
        }
        else
        {
            Debug.Log("brak surowcow");
        }

        

    }

}
