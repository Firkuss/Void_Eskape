using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject sManager;
    public static ShopManager inst;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        sManager = GameObject.Find("ShopManager");

        DontDestroyOnLoad(sManager);
        shopPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressRestart()
    {
        shopPanel.SetActive(false);

        SceneManager.LoadScene("SampleScene");
    }
}
