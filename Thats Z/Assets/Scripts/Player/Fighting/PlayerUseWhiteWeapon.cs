using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseWhiteWeapon : MonoBehaviour
{
    public int damage;
    public float convenience;
    public int durability;
    public Animator WeaponAnimator;
    public GameObject WhiteWeapon;

    private float lastTimeAttack;
    public void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (Input.GetButtonDown("Fire1")  && Time.time > lastTimeAttack + convenience)
            {
                if (durability > 0)
                {
                    Attack();
                    lastTimeAttack = Time.deltaTime;
                }
            }
        }
    }

    private void Attack()
    {
        WeaponAnimator.SetBool("isAttack",true);
        
    }
    
}
