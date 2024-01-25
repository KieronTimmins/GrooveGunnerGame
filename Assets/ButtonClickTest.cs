using UnityEngine;
using UnityEngine.UI;

public class ButtonClickTest : MonoBehaviour
{
    public Button myButton;

    void Start()
    {
        myButton.onClick.AddListener(HandleButtonClick);
    }

    void HandleButtonClick()
    {
        Debug.Log("Button Clicked!");
    }
}
