using UnityEngine;

public class EnemySpawner : UnitSpawner
{
    [SerializeField] private GameObject[] enemyPrefabs;   // 3 enemigos
    protected override GameObject[] Prefabs => enemyPrefabs;
}
