using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;
    public int itemID;
    public int price;
}

//
// broń palna, amunicja, broń biała, jedzenie, apteczki, surowce, narzędzia, ochrona lewej ręki, ochrona prawej ręki, spodnie
// 

[CreateAssetMenu(fileName = "New Firearm", menuName = "Items/Firearm")]
public class Firearm : Item
{
    public int damage;
    public float fireRate;
    public int MagSize;
}

[CreateAssetMenu(fileName = "New Ammo", menuName = "Items/Ammo")]
public class Ammo : Item
{
    public Firearm compatibleFirearm;
}
