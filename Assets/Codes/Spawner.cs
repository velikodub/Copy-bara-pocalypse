using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private GameObject capybaraPrefab;
    [SerializeField] private Transform capybaraRoot;

    public int capybaraCount = 0;

    void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        SpawnInitialPair();
    }
    private void SpawnInitialPair()
    {
        Vector2 leftPosition = new Vector2(-3f, 0f);
        Vector2 rightPosition = new Vector2(3f, 0f);

        GameObject leftCapybaraObj = Instantiate(capybaraPrefab, leftPosition, Quaternion.identity, capybaraRoot);
        GameObject rightCapybaraObj = Instantiate(capybaraPrefab, rightPosition, Quaternion.identity, capybaraRoot);

        Capybara leftCapybara = leftCapybaraObj.GetComponent<Capybara>();
        Capybara rightCapybara = rightCapybaraObj.GetComponent<Capybara>();

        leftCapybara.SetCustomTarget(rightPosition);
        rightCapybara.SetCustomTarget(leftPosition);
    }
    public void SpawnCapybara(Vector2 position)
    {
        Vector2 spawnPos = position + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        Instantiate(capybaraPrefab, spawnPos, Quaternion.identity, capybaraRoot);
    }
}
