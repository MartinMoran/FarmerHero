using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] soldierPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float initialDelay = 2f;
    [SerializeField] private float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnSoldier), initialDelay, spawnInterval);
    }

    private void SpawnSoldier()
    {
        if (soldierPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            return;
        }

        int soldierIndex = Random.Range(0, soldierPrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(soldierPrefabs[soldierIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
    }
}
