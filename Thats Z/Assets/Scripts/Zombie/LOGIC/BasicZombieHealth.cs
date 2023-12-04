using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicZombieHealth : MonoBehaviour
{
    
    private float health = 100;

    public void TakeDamage(float Damage)
    {
        Debug.Log("Ala ~ zombie");
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
