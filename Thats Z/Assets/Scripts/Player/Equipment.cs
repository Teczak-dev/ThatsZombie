using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Item pustySlot;
    public Item[] Weapons = new Item[3];
    
    public Item[] FastSlots = new Item[4];

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Weapons[i] = pustySlot;
        }

        for (int i = 0; i < 4; i++)
        {
            FastSlots[i] = pustySlot;
        }
    }

    public void SetWeapon(int index, Item item)
    {
        Weapons[index] = item;
    }

    public void removeWeapon(int index)
    {
        Weapons[index] = pustySlot;
    }
    
    
}
