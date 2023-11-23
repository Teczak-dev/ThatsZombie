using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungrySys : MonoBehaviour
{
    public PlayerController pc;
    public PlayerHealth ph;
    public int hungry = 10000;
    public int maxhungry = 10000;
    public Slider hungrySlider;
    public int HUNGRYCOST = 10;
    public float szybkoscGlodu = 5f;



    private float lastTimeTakeHungry = 0;
    // Update is called once per frame
    void Update()
    {
        if (!pc.isPause)
        {
            Debug.Log("Nie ma pause mozna glodowac :D");
            if (Time.time>lastTimeTakeHungry+szybkoscGlodu)
            {
                Debug.Log("GUL GUL GUL");
                if (hungry>0)
                {
                    RemoveHungry(HUNGRYCOST);
                }

                if (hungry <= 0)
                {
                    ph.TakeDamage(10);
                }
                lastTimeTakeHungry = Time.time;
            }
        }

    }

    private void UpdateSlider()
    {
        hungrySlider.value = hungry;
    }

    public void RemoveHungry(int cost)
    {
        hungry -= cost;
        UpdateSlider();
    }

    public void AddHungry(int amount)
    {
        hungry += amount;
    }
    
}
