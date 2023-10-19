using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint; // Transformacja punktu wystrzału broni
    public GameObject pociskPrefab; // Prefabrykat pocisku
    public float silaPocisku = 10f; // Siła strzału broni
    public float szybkoscStrzelania = 0.5f; // Czas między kolejnymi strzałami
    public Text ammoTxt;
    public Text magTxt;
    public GameObject AmmoDBs;
    

    private float czasOstatniegoStrzalu = 0f;
    private int magAmmo = 0;
    private AmmoDB _ammodb;

    private void Start()
    {
        _ammodb = AmmoDBs.GetComponent<AmmoDB>();
        ammoTxt.text = _ammodb.GetAmmo("pistol").ToString();
        magTxt.text = magAmmo + "/10";
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            
            // Sprawdź, czy możemy strzelać
            if (Input.GetButtonDown("Fire1") && Time.time > czasOstatniegoStrzalu + szybkoscStrzelania)
            {
                if (magAmmo > 0)
                {
                    Strzel(); // Wywołaj funkcję strzelania
                    czasOstatniegoStrzalu = Time.time; // Zaktualizuj czas ostatniego strzału
                    magAmmo--;
                }
                else
                {
                    if (_ammodb.GetAmmo("pistol") >= 10)
                    {
                        magAmmo = 10;
                        _ammodb.RemoveAmmo("pistol",10);
                    }
                }
                ammoTxt.text = _ammodb.GetAmmo("pistol").ToString();
                magTxt.text = $"{magAmmo.ToString()}/10";

            }
            
        }
    }

    void Strzel()
    {
        // Stworzenie pocisku na pozycji firePoint
        GameObject pocisk = Instantiate(pociskPrefab, firePoint.position, firePoint.rotation);

        // Dodaj siłę do pocisku
        Rigidbody rb = pocisk.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * silaPocisku, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Prefab pocisku nie ma komponentu Rigidbody!");
        }

        // Zniszcz pocisk po pewnym czasie (na przykład 2 sekundy)
        Destroy(pocisk, 2f);
    }
}
