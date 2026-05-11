using System.Collections;
using UnityEngine;

public class ResoursesManager : MonoBehaviour
{
    public static ResoursesManager Instance;

    public int coins{get; private set;}
    public int mandarins {get; private set;}
    [SerializeField] private int startMandarins = 50;

    private int clickIncome = 1;
    private int amountToConsumePerCapybara = 1;
    private float consumeInterval = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(ConsumeRoutine());
        UpdateCoins();
        UpdateMandarins();
    }
    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoins();
    }
    public void AddCoins()
    {
        coins += clickIncome;
        UpdateCoins();
    }
    private IEnumerator ConsumeRoutine()
    {
        mandarins = startMandarins;
        while(!GameManager.Instance.isGameOver)
        {
            yield return new WaitForSeconds(consumeInterval);
            ConsumeMandarins();
        }
    }
    private void UpdateCoins()
    {
        ResourcesUI.Instance.UpdateCoins(coins);
    }
    private void UpdateMandarins()
    {
        ResourcesUI.Instance.UpdateMandarins(mandarins);
    }
    private void ConsumeMandarins()
    {
        int amountToConsume = Spawner.Instance.capybaraCount*amountToConsumePerCapybara;
        mandarins -= amountToConsume;

        if (mandarins <= 0)
        {
            mandarins = 0; 
            GameManager.Instance.GameOver();
        }
        UpdateMandarins();
    }
    private void ConsumeMandarins(int amount)
    {
        mandarins -= amount;

        if (mandarins <= 0)
        {
            mandarins = 0; 
            GameManager.Instance.GameOver();
        }
        UpdateMandarins();
    }
}
