using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public float damage = 20;


    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "BasicZombie")
        {
            BasicZombieHealth zombieH = other.gameObject.GetComponent<BasicZombieHealth>();
            zombieH.TakeDamage(damage);
            Debug.Log("Zaatakowano zombie");
            Destroy(this.gameObject);
        }
        
    }

    public void SetDamage(float dam)
    {
        damage = dam;
    }
}
