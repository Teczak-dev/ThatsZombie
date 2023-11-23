using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int health = 100;
    public int maxhealth = 100;
    public Slider HPSLIDER;
    public PlayerUIManager pUIm;

    public void TakeDamage(int damage)
    {
        health -= damage;
        HPSLIDER.value = health;
        if (health <= 0)
        {
            DEATH();
        }
    }

    private void DEATH()
    {
        
        pUIm.Death();
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddHealth(int amount)
    {
        health += amount;
        HPSLIDER.value = health;
    }
    
}
