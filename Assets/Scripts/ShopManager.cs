using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;
    public TMP_Text currencyText;
    public int playerCurrency = 0;

    void Start()
    {
        shopPanel.SetActive(false);
        UpdateCurrencyUI();
    }

    void Update()
    {
        UpdateCurrencyUI();
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 2f;
        UpdateCurrencyUI();
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BuyItem(int cost)
    {
        if (playerCurrency >= cost)
        {
            playerCurrency -= cost;
            UpdateCurrencyUI();
            Debug.Log("Item purchased!");           
        }
        else
        {
            Debug.Log("Not enough currency!");
        }
    }

    void UpdateCurrencyUI()
    {
        currencyText.text = "Plasma: " + playerCurrency;
    }
}
