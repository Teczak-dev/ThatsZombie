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
// broń palna, amunicja, broń biała, jedzenie, używki, apteczki, surowce, narzędzia, ochrona lewej ręki, ochrona prawej ręki, spodnie
// 

[CreateAssetMenu(fileName = "New Firearm", menuName = "Items/Firearm")]
public class Firearm : Item
{
    public int damage;
    public float fireRate;
    public bool isShort;
    public string weaponType;
    public int MagSize;
}

[CreateAssetMenu(fileName = "New Ammo", menuName = "Items/Ammo")]
public class Ammo : Item
{
    public Firearm compatibleFirearm;
}

[CreateAssetMenu(fileName = "New White Weapon", menuName = "Items/White Weapons")]
public class WhiteWeapon : Item
{
    public int damage;
    public int convenience;//Poręczność
    public int durability;//Wytrzymałość
}

[CreateAssetMenu(fileName = "New Food", menuName = "Items/Food")]
public class Food : Item
{
    public int Hungry;
    public int Water;
}

[CreateAssetMenu(fileName = "New First Aid Kit", menuName = "Items/First Aid Kit")]
public class FirstAidKit : Item
{
    public int Health;
}


