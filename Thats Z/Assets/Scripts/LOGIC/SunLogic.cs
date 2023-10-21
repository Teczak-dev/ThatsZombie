using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunLogic : MonoBehaviour
{
    public Light sunLight;
    public Color dayColor;
    public Color nightColor;
    public float dayLengthMinutes = 24;

    private float currentTime = 0;

    void Update()
    {
        currentTime += Time.deltaTime;
        float timeRatio = currentTime / (dayLengthMinutes * 60); // Przeliczenie na zakres 0-1

        // Obrót światła słonecznego
        float rotationAngle = 360 * timeRatio;
        sunLight.transform.rotation = Quaternion.Euler(rotationAngle, 0, 0);

        // Zmiana koloru światła
        sunLight.color = Color.Lerp(nightColor, dayColor, timeRatio);

        if (currentTime >= dayLengthMinutes * 60)
        {
            currentTime = 0;
        }
    }
}
