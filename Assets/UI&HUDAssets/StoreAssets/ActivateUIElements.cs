using UnityEngine;
using UnityEngine.UI;

public class ActivateUIElements : MonoBehaviour
{
    public GameObject[] elementsToToggle;
    private bool areElementsActive = false;

    void Start()
    {
        // Ensure all elements are initially deactivated
        DeactivateUIElements();
    }

    void DeactivateUIElements()
    {
        foreach (GameObject element in elementsToToggle)
        {
            if (element != null)
            {
                element.SetActive(false);
            }
        }
        areElementsActive = false;
    }

    public void ToggleElementsOnClick()
    {
        if (areElementsActive)
        {
            DeactivateUIElements();
        }
        else
        {
            ActivatedUIElements();
        }
    }

    void ActivatedUIElements()
    {
        foreach (GameObject element in elementsToToggle)
        {
            if (element != null)
            {
                element.SetActive(true);
            }
        }
        areElementsActive = true;
    }
}
