using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Spawn Prefabs")]
    [SerializeField] private GameObject[] _powerupPrefabs;
    [SerializeField] private GameObject[] _enemyPrefabs;

    [Header("Spawn Timing")]
    [SerializeField] private float _powerupSpawnMin = 3f;
    [SerializeField] private float _powerupSpawnMax = 8f;
    [SerializeField] private float _enemySpawnDelay = 1.5f;

    [Header("Spawn Boundaries")]
    [SerializeField] private float _spawnRangeX = 17f;
    [SerializeField] private float _spawnRangeZ = 9f;
    [SerializeField] private float _enemySpawnZ = 18f;

    [Header("Game Over Settings")]
    [SerializeField] private float _gameOverDelay = 2f;

    public bool isGameActive = true;
    public int Score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Score = 0;
        if (MainUIHandler.Instance != null)
        {
            MainUIHandler.Instance.UpdateScore(Score);
        }
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnEnemyRoutine());
    }

    public void AddScore(int points)
    {
        if (!isGameActive) return;
        Score += points;
        if (MainUIHandler.Instance != null)
        {
            MainUIHandler.Instance.UpdateScore(Score);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (isGameActive)
        {
            float randomDelay = Random.Range(_powerupSpawnMin, _powerupSpawnMax);
            yield return new WaitForSeconds(randomDelay);
            if (_powerupPrefabs != null && _powerupPrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, _powerupPrefabs.Length);
                Instantiate(_powerupPrefabs[randomIndex], RandomPowerupPos(), _powerupPrefabs[randomIndex].transform.rotation);
            }
            
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(_enemySpawnDelay);
            if (_enemyPrefabs != null && _enemyPrefabs.Length > 0)
            {
                int randomIndex = Random.Range(0, _enemyPrefabs.Length);
                Instantiate(_enemyPrefabs[randomIndex], RandomEnemyPos(), _enemyPrefabs[randomIndex].transform.rotation);
            }
        }
    }

    private IEnumerator GameOverRoutine()
    {
        MainUIHandler.Instance.HideGameUI();
        yield return new WaitForSeconds(_gameOverDelay);
        if (MainUIHandler.Instance != null)
        {
            MainUIHandler.Instance.ShowGameOverScreen(Score);
        }

    }

    private Vector3 RandomPowerupPos()
    {
        float randomX = Random.Range(-_spawnRangeX, _spawnRangeX);
        float randomZ = Random.Range(-_spawnRangeZ, _spawnRangeZ);
        return new Vector3(randomX, 0.5f, randomZ);
    }
    private Vector3 RandomEnemyPos()
    {
        float randomX = Random.Range(-_spawnRangeX, _spawnRangeX);
        return new Vector3(randomX, 0.5f, _enemySpawnZ);
    }
    public void TriggerGameOver()
    {
        if (!isGameActive) return;

        isGameActive = false;
        StopAllCoroutines();
        StartCoroutine(GameOverRoutine());
    }
}
