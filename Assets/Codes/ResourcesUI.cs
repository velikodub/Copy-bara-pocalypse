using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    public static ResourcesUI Instance { get; private set; }
    [Header("UI Elements")]
    [SerializeField] private Text coinsText;
    [SerializeField] private Text mandarinsText;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }
    public void UpdateCoins(int coins)
    {
        coinsText.text = "Coins: " + coins.ToString();
    }
    public void UpdateMandarins(int mandarins)
    {
        mandarinsText.text = "Mandarins: " + mandarins.ToString();
    }
}
