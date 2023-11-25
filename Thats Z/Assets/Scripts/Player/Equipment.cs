using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public GameObject WeaponUI;
    public Item pustySlot;
    public GameObject[] PlayerWeapons;
    public Item[] Weapons = new Item[3];
    public int[] WeapInd = new int[3];
    
    public Item[] FastSlots = new Item[4];

    private bool isWO1 = false;
    private bool isWO2 = false;
    private bool isWO3 = false;

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Weapons[i] = pustySlot;
        }
        for (int i = 0; i < 3; i++)
        {
            WeapInd[i] = -1;
        }
        for (int i = 0; i < 4; i++)
        {
            FastSlots[i] = pustySlot;
        }
    }

    private void Update()
    {
        #region Weap1
        
            if (Input.GetKeyDown(KeyCode.Alpha1) && Weapons[0] != pustySlot)
            {
                if (!isWO1)
                {
                    for (int i = 0; i < PlayerWeapons.Length; i++)
                    {
                        PlayerWeapons[i].SetActive(false);
                        if (PlayerWeapons[i].GetComponent<PlayerShooting>().Wname == Weapons[0].name)
                        {
                            PlayerWeapons[i].SetActive(true);
                            PlayerShooting ps = PlayerWeapons[i].GetComponent<PlayerShooting>();
                            ps.changeWeapon(((Firearm)Weapons[0]).weaponType, ((Firearm)Weapons[0]).MagSize, ((Firearm)Weapons[0]).damage, ((Firearm)Weapons[0]).fireRate);
                            isWO1 = true;
                            isWO2 = false;
                            isWO3 = false;
                            WeaponUI.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < PlayerWeapons.Length; i++)
                    {
                        PlayerWeapons[i].SetActive(false);
                        
                    }

                    isWO1 = false;
                    WeaponUI.SetActive(false);
                }
            }
        #endregion Weap1

        #region Weap2
        
            if (Input.GetKeyDown(KeyCode.Alpha2) && Weapons[1] != pustySlot)
            {
                if (!isWO2)
                {
                    for (int i = 0; i < PlayerWeapons.Length; i++)
                    {
                        PlayerWeapons[i].SetActive(false);
                        if (PlayerWeapons[i].GetComponent<PlayerShooting>().Wname == Weapons[1].name)
                        {
                            PlayerWeapons[i].SetActive(true);
                            PlayerShooting ps = PlayerWeapons[i].GetComponent<PlayerShooting>();
                            ps.changeWeapon(((Firearm)Weapons[1]).weaponType, ((Firearm)Weapons[1]).MagSize, ((Firearm)Weapons[1]).damage, ((Firearm)Weapons[1]).fireRate);
                            isWO2 = true;
                            
                            isWO1 = false;
                            isWO3 = false;
                            WeaponUI.SetActive(true);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < PlayerWeapons.Length; i++)
                    {
                        PlayerWeapons[i].SetActive(false);
                        
                    }

                    isWO2 = false;
                    WeaponUI.SetActive(false);
                }
            }
        #endregion Weap2
        
        #region Weap3
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && Weapons[2] != pustySlot)
        {
            if (!isWO3)
            {
                for (int i = 0; i < PlayerWeapons.Length; i++)
                {
                    PlayerWeapons[i].SetActive(false);
                    if (PlayerWeapons[i].GetComponent<PlayerShooting>().Wname == Weapons[2].name)
                    {
                        PlayerWeapons[i].SetActive(true);
                        PlayerShooting ps = PlayerWeapons[i].GetComponent<PlayerShooting>();
                        ps.changeWeapon(((Firearm)Weapons[2]).weaponType, ((Firearm)Weapons[2]).MagSize, ((Firearm)Weapons[2]).damage, ((Firearm)Weapons[2]).fireRate);
                        isWO3 = true;
                        
                        isWO2 = false;
                        isWO1 = false;
                        WeaponUI.SetActive(true);
                    }
                }
            }
            else
            {
                for (int i = 0; i < PlayerWeapons.Length; i++)
                {
                    PlayerWeapons[i].SetActive(false);
                        
                }

                isWO3 = false;
                WeaponUI.SetActive(false);
            }
        }
        #endregion Weap3
        
    }

    public void SetWeapon(int Windex, Item item,int index)
    {
        Windex -= 1;
        if (WeapInd[Windex] != index)
        {
            
            
            if (Windex == 0 && Weapons[1] == item) RemoveWeapon(WeapInd[1]);
            else if(Windex == 1 && Weapons[0] == item) RemoveWeapon(WeapInd[0]);
            Weapons[Windex] = item;
            WeapInd[Windex] = index;
            Debug.Log(Windex + ", " + Weapons[Windex]);
                    

        }

        else if (Weapons[Windex] != pustySlot && WeapInd[Windex] == index)
        {
            RemoveWeapon(index);
        }
    }

    public void RemoveWeapon(int index)
    {
        for (int i = 0; i < 3; i++)
        {
            if (WeapInd[i] == index)
            {
                Weapons[i] = pustySlot;
                WeapInd[i] = -1;
            }
        }
    }
    
    
}
