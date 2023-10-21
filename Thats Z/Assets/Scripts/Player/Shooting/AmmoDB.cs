using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDB : MonoBehaviour
{

    private int PistolAmmo = 999;
    private int SMGAmmo = 500;


    public int GetAmmo(string type)
    {
        if (type == "Pistol")
        {
            return PistolAmmo;
        }

        if (type == "Smg")
        {
            return SMGAmmo;
        }

        return 0;
    }

    public void RemoveAmmo(string type, int amount)
    {
        if (type == "Pistol")
        {
            PistolAmmo -= amount;
        }

        else if (type == "Smg")
        {
            SMGAmmo -= amount;
        }
    }
    
    
}
