using UnityEngine;

/// Clase abstracta compartida para spawners de unidades. Las subclases aportan sus prefabs.
public abstract class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float initialDelay = 2f;
    [SerializeField] private float spawnInterval = 5f;

    protected abstract GameObject[] Prefabs { get; }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnUnit), initialDelay, spawnInterval);
    }

    private void SpawnUnit()
    {
        if (Prefabs == null || Prefabs.Length == 0 || spawnPoints == null || spawnPoints.Length == 0) return;

        GameObject prefab = Prefabs[Random.Range(0, Prefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
