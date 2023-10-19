using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BasicZombie")
        {
            BasicZombieHealth zombieH = other.gameObject.GetComponent<BasicZombieHealth>();
            zombieH.TakeDamage(damage);
            Debug.Log("Zaatakowano zombie");
        }
    }
}
