using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] items;
    public KeyCode[] itemActivationKeys;

    private int currentItemIndex = -1;

    void Update()
    {

        for (int i = 0; i < itemActivationKeys.Length; i++)
        {
            if (Input.GetKeyDown(itemActivationKeys[i]))
            {
                SetActiveItem(i);
            }
        }
    }

    void SetActiveItem(int index)
    {

        if (currentItemIndex >= 0 && currentItemIndex < items.Length)
        {
            items[currentItemIndex].SetActive(false);
        }


        if (index >= 0 && index < items.Length)
        {
            items[index].SetActive(true);
            currentItemIndex = index;
        }
        else
        {
            currentItemIndex = -1;
        }
    }
}
