using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;   // 3 enemigos
    [SerializeField] private Transform[] spawnPoints;     // varios puntos
    [SerializeField] private float initialDelay = 2f;
    [SerializeField] private float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), initialDelay, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            return;
        }

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemyPrefabs[enemyIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
    }
}
