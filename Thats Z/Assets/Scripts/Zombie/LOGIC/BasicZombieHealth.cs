using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicZombieHealth : MonoBehaviour
{
    
    private int health = 100;

    public void TakeDamage(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            DEATH();
        }
    }

    private void DEATH()
    {
        Destroy(this.gameObject);
    }
}
