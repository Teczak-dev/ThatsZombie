using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public int maksymalnaIloscPrzedmiotow = 10;
    public int iloscWolnegoMiejsca = 10;
    private List<Item> przedmioty = new List<Item>();
    public List<Image> sloty = new List<Image>();

    public bool DodajPrzedmiot(Item item)
    {
        if (przedmioty.Count < maksymalnaIloscPrzedmiotow && iloscWolnegoMiejsca<=10)
        {
            przedmioty.Add(item);
            sloty[przedmioty.Count - 1].sprite = przedmioty[przedmioty.Count - 1].icon;
            Debug.Log("Dodano " + item.name + " do ekwipunku.");
            return true;
        }
        else
        {
            Debug.Log("Ekwipunek jest pełny.");
            return false;
        }
    }

    public void UsunPrzedmiot(Item item)
    {
        if (przedmioty.Contains(item))
        {
            przedmioty.Remove(item);
            Debug.Log("Usunięto " + item.name + " z ekwipunku.");
        }
        else
        {
            Debug.Log(item.name + " nie znajduje się w ekwipunku.");
        }
    }

    public void WyswietlEkwipunek()
    {
        Debug.Log("Zawartość ekwipunku:");
        foreach (Item item in przedmioty)
        {
            Debug.Log("- " + item.name);
        }
    }
}
