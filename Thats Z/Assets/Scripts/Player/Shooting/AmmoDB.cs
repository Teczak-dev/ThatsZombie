using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDB : MonoBehaviour
{

    private int PistolAmmo = 200;
    private int bowAmmo = 30;
    private int shotgunAmmo = 77;
    private int SMGAmmo = 500;
    private int RifleAmmo = 999;


    public int GetAmmo(string type)
    {
        if (type == "Pistol") return PistolAmmo;
        

        if (type == "Smg") return SMGAmmo;
        

        if (type == "Rifle") return RifleAmmo;
        

        if (type == "Shotgun") return shotgunAmmo;

        if (type == "Bow") return bowAmmo;

        return 0;
    }

    public void RemoveAmmo(string type, int amount)
    {
        if (type == "Pistol") PistolAmmo -= amount;

        else if (type == "Smg") SMGAmmo -= amount;
        
        else if (type == "Rifle") RifleAmmo -= amount;
        
        else if (type == "Shotgun") shotgunAmmo -= amount;
        
        else if (type == "Bow") bowAmmo -= amount;
        
    }

    public void addAmmo(string type, int amount)
    {
        if (type == "Pistol") PistolAmmo += amount;

        else if (type == "Smg") SMGAmmo += amount;
        
        else if (type == "Rifle") RifleAmmo += amount;
        
        else if (type == "Shotgun") shotgunAmmo += amount;
        
        else if (type == "Bow") bowAmmo += amount;
        
    }
    
    
}
