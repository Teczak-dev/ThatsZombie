using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDB : MonoBehaviour
{

    private int PistolAmmo = 999;


    public int GetAmmo(string type)
    {
        if (type == "pistol")
        {
            return PistolAmmo;
        }

        return 0;
    }

    public void RemoveAmmo(string type, int amount)
    {
        if (type == "pistol")
        {
            PistolAmmo -= amount;
        }
    }
    
    
}
