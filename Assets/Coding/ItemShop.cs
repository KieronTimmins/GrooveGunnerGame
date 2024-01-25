using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ItemShop : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI notEnoughMoneyText;

    public GameObject purchaseSuccessfulScreen; // Reference to the purchase successful screen
    private Button continueButton; // Reference to the continue button

    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public int itemCost;
        public Button purchaseButton;
        public Button secondButton; // Add a reference to the second button
    }

    public ShopItem[] shopItems;

    void Start()
    {
        UpdateCurrencyText();

        for (int i = 0; i < shopItems.Length; i++)
        {
            int itemIndex = i;
            shopItems[i].purchaseButton.onClick.AddListener(() => PurchaseItem(itemIndex));
        }

        // Automatically find the continue button
        continueButton = purchaseSuccessfulScreen.GetComponentInChildren<Button>();
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(ContinueButtonClicked);
        }
        else
        {
            Debug.LogError("Continue button not found in the purchase successful screen.");
        }
    }

    void UpdateCurrencyText()
    {
        currencyText.text = "Currency: " + PlayerProfile.Instance.Currency;
    }

    void SetNotEnoughMoneyMessage(string message)
    {
        notEnoughMoneyText.text = message;
        notEnoughMoneyText.gameObject.SetActive(true);

        StartCoroutine(HideNotEnoughMoneyMessage(2f)); // Hide after 2 seconds

        foreach (var item in shopItems)
        {
            item.purchaseButton.interactable = true;
        }
    }

    IEnumerator HideNotEnoughMoneyMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        notEnoughMoneyText.text = string.Empty;
        notEnoughMoneyText.gameObject.SetActive(false);
    }

    void ContinueButtonClicked()
    {
        notEnoughMoneyText.text = string.Empty;
        notEnoughMoneyText.gameObject.SetActive(false);

        foreach (var item in shopItems)
        {
            item.purchaseButton.interactable = true;
        }

        // Deactivate the purchase successful screen
        purchaseSuccessfulScreen.SetActive(false);
    }

    public void PurchaseItem(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= shopItems.Length)
        {
            Debug.LogError("Invalid item index");
            return;
        }

        int itemCost = shopItems[itemIndex].itemCost;

        if (PlayerProfile.Instance.Currency >= itemCost)
        {
            PlayerProfile.Instance.AddCurrency(-itemCost);
            UpdateCurrencyText();

            shopItems[itemIndex].purchaseButton.interactable = false;
            shopItems[itemIndex].secondButton.interactable = false; // Disable the second button

            // Activate the purchase successful screen
            purchaseSuccessfulScreen.SetActive(true);
        }
        else
        {
            SetNotEnoughMoneyMessage("Not Enough Harmony Tokens");
        }
    }
}

