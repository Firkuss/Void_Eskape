using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;
    public TMP_Text currencyText;
    public int playerCurrency;
    public static ShopManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Plus d'une instance dans le jeu");
            return;
        }

        DontDestroyOnLoad(transform);

        instance = this;
        shopPanel.SetActive(false);
        UpdateCurrencyUI();
    }

    void Update()
    {
        UpdateCurrencyUI();
    }

    public void OpenShop()
    {
        Time.timeScale = 0.1f;
        shopPanel.SetActive(true);
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
        currencyText.text = ":" + playerCurrency;
    }
}
