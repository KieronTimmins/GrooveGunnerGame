using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public GameObject itemObject;
        public KeyCode activationKey;
        public Image[] uiImages; // Two UI images for each item
    }

    public Item[] items;

    private int currentItemIndex = -1;

    void Update()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown(items[i].activationKey))
            {
                SetActiveItem(i);
                UpdateUIVisibility();
            }
        }
    }

    void SetActiveItem(int index)
    {
        if (currentItemIndex >= 0 && currentItemIndex < items.Length)
        {
            items[currentItemIndex].itemObject.SetActive(false);
        }

        if (index >= 0 && index < items.Length)
        {
            items[index].itemObject.SetActive(true);
            currentItemIndex = index;
        }
        else
        {
            currentItemIndex = -1;
        }
    }

    void UpdateUIVisibility()
    {
        // Hide all UI images
        foreach (Item item in items)
        {
            foreach (Image uiImage in item.uiImages)
            {
                uiImage.gameObject.SetActive(false);
            }
        }

        // Show the UI images corresponding to the active item
        if (currentItemIndex >= 0 && currentItemIndex < items.Length)
        {
            foreach (Image uiImage in items[currentItemIndex].uiImages)
            {
                uiImage.gameObject.SetActive(true);
            }
        }
    }
}
