using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject prefabToActivate; // Assign your prefab in the Inspector

    void Start()
    {
        // Ensure the prefab is deactivated at the start
        if (prefabToActivate != null)
        {
            prefabToActivate.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // The pointer (mouse) is over the button
        if (prefabToActivate != null)
        {
            prefabToActivate.SetActive(true); // Activate the prefab
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // The pointer (mouse) is no longer over the button
        if (prefabToActivate != null)
        {
            prefabToActivate.SetActive(false); // Deactivate the prefab
        }
    }
}
