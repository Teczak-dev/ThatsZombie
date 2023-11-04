using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public ScrollRect scrollView;
    public GameObject itemPrefab;

    private int maxInventorySize = 10;
    private int currentInventorySize = 5;

    private void Start()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        foreach (Transform child in scrollView.content.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentInventorySize; i++)
        {
            GameObject item = Instantiate(itemPrefab, scrollView.content);
            Button itemButton = item.GetComponentInChildren<Button>();
            
            // Przypisz ID elementu jako nazwę przycisku
            itemButton.name = i.ToString();

            // Dodaj obsługę kliknięcia przycisku z przekazaniem ID
            itemButton.onClick.AddListener(() => OnItemClick(int.Parse(itemButton.name)));
        }
    }

    public void UpgradeInventory()
    {
        if (currentInventorySize < maxInventorySize)
        {
            currentInventorySize++;
            GameObject item = Instantiate(itemPrefab, scrollView.content);
            Button itemButton = item.GetComponentInChildren<Button>();
            
            // Przypisz ID elementu jako nazwę przycisku
            itemButton.name = (currentInventorySize - 1).ToString();
            
            // Dodaj obsługę kliknięcia przycisku z przekazaniem ID
            itemButton.onClick.AddListener(() => OnItemClick(int.Parse(itemButton.name)));
        }
    }

    // Obsługa kliknięcia przycisku z przekazaniem ID
    private void OnItemClick(int itemID)
    {
        Debug.Log($"Selected Item ID:  {itemID}");
        // Tutaj możesz wykonać dodatkowe akcje na podstawie wybranego ID
    }
}
