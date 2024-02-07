using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public GameObject Camera;
    public Transform firePoint; // Transformacja punktu wystrzału broni
    public GameObject pociskPrefab; // Prefabrykat pocisku
    public float silaPocisku = 10f; // Siła strzału broni
    public float szybkoscStrzelania = 0.5f; // Czas między kolejnymi strzałami
    public Text ammoTxt;
    public Text magTxt;
    public int maxMagAmmo = 0;
    public string weaponType = "";
    public GameObject AmmoDBs;
    public string Wname;
    
    

    private float czasOstatniegoStrzalu = 0f;
    private int magAmmo = 0;
    private AmmoDB _ammodb;

    public void changeWeapon(string wT, int mA,int D, float sS )
    {
        weaponType = wT;
        maxMagAmmo = mA;
        szybkoscStrzelania = sS;
        silaPocisku = D;
        _ammodb = AmmoDBs.GetComponent<AmmoDB>();
        ammoTxt.text = _ammodb.GetAmmo(weaponType).ToString();
        magTxt.text = magAmmo + "/"+maxMagAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            // Sprawdź, czy możemy strzelać
            if (Input.GetButton("Fire1") && Time.time > czasOstatniegoStrzalu + szybkoscStrzelania)
            {
                if (magAmmo > 0)
                {
                    Debug.Log("Strzelanie");
                    Strzel(); // Wywołaj funkcję strzelania
                    czasOstatniegoStrzalu = Time.time; // Zaktualizuj czas ostatniego strzału
                    magAmmo--;
                    if(magAmmo == 0) Reload();
                }
                else
                {
                    Reload();
                }
                ammoTxt.text = _ammodb.GetAmmo(weaponType).ToString();
                magTxt.text = $"{magAmmo.ToString()}/{maxMagAmmo.ToString()}";

            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
                ammoTxt.text = _ammodb.GetAmmo(weaponType).ToString();
                magTxt.text = $"{magAmmo.ToString()}/{maxMagAmmo.ToString()}";
            }
            
        }
    }

    void Strzel()
    {
        // Stworzenie pocisku na pozycji firePoint
        GameObject pocisk = Instantiate(pociskPrefab, firePoint.position, firePoint.rotation);
        pocisk.GetComponent<BulletLogic>().SetDamage(silaPocisku);

        // Dodaj siłę do pocisku
        Rigidbody rb = pocisk.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * 5, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Prefab pocisku nie ma komponentu Rigidbody!");
        }

        // Zniszcz pocisk po pewnym czasie (na przykład 2 sekundy)
        Destroy(pocisk, 2f);
    }

    private void Reload()
    {
        if (_ammodb.GetAmmo(weaponType) >= maxMagAmmo)
        {
            magAmmo = maxMagAmmo;
            _ammodb.RemoveAmmo(weaponType,maxMagAmmo);
        }
        else
        {
            magAmmo = _ammodb.GetAmmo(weaponType);
            _ammodb.RemoveAmmo(weaponType,magAmmo);
        }
    }
}
