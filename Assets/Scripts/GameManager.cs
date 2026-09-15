using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Spawn Prefabs")]
    [SerializeField] private GameObject[] _powerupPrefabs;
    [SerializeField] private GameObject[] _enemyPrefabs;

    [Header("Spawn Timing")]
    [SerializeField] private float _powerupSpawnMin = 3f;
    [SerializeField] private float _powerupSpawnMax = 8f;
    [SerializeField] private float _enemySpawnDelay = 2f;

    [Header("Spawn Boundaries")]
    [SerializeField] private float _spawnRangeX = 17f;
    [SerializeField] private float _spawnRangeZ = 9f;
    [SerializeField] private float _enemySpawnZ = 15f;

    public bool isGameActive = true;

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
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnEnemyRoutine());
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
}
