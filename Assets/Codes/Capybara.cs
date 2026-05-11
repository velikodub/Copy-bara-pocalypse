using System.Collections;
using UnityEngine;

public class Capybara : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]private float speed = 3f;
    [SerializeField]private float padding = 0.5f;
    [SerializeField]private Camera mainCamera;
    [Header("Breeding")]
    [SerializeField] private float breedingCooldown = 5f;

    private float minX, maxX, minY, maxY;
    private Vector2 targetPosition;
    private bool isOnCooldown = false;
    private bool hasCustomTarget = false;

    private void Start()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        CalculateBounds();

        if (!hasCustomTarget)
        {
            SetNewRandomTarget();
        }

        Spawner.Instance.capybaraCount++;
    }
    private void OnDestroy()
    {
        if(Spawner.Instance != null)
        {
            Spawner.Instance.capybaraCount--;
        }
    }
    private void Update()
    {
        MoveTowardsTarget();
    }
    IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        
        // Просто ждем нужное количество секунд
        yield return new WaitForSeconds(breedingCooldown);
        
        isOnCooldown = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Capybara other = collision.GetComponent<Capybara>();

        if (other != null)
        {
            if(this.isOnCooldown || other.isOnCooldown)
            {
                return;
            }

            this.StartCoroutine(this.CooldownRoutine());
            other.StartCoroutine(other.CooldownRoutine());

            Spawner.Instance.SpawnCapybara(transform.position);
        }
    }
    public void SetCustomTarget(Vector2 target)
    {
        targetPosition = target;
        hasCustomTarget = true;
    }
    private void OnMouseDown()
    {
        if(Spawner.Instance.capybaraCount > 2)
        {
            ResoursesManager.Instance.AddCoins();
            Destroy(gameObject);
        }
    }
    private void CalculateBounds()
    {
        float camHeight = mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        minY = mainCamera.transform.position.y - camHeight + padding;
        maxY = mainCamera.transform.position.y + camHeight - padding;
        minX = mainCamera.transform.position.x - camWidth + padding;
        maxX = mainCamera.transform.position.x + camWidth - padding;
    }
    private void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewRandomTarget();
        }
    }
    private void SetNewRandomTarget()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
    }
}
